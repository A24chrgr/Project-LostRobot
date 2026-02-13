using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class DirectiveManager : MonoBehaviour
    {
        public List<Directive> directives;
        private UIManager uiManager;
        
        [HideInInspector] public int currentDirectiveIndex;
        [HideInInspector] public int currentSubDirectiveIndex;
        
        [HideInInspector] public UnityEvent DirectiveComplete = new UnityEvent();
        [HideInInspector] public UnityEvent SubDirectiveComplete = new UnityEvent();
        
        void Start()
        {
            uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

            if (directives == null || directives.Count == 0)
            {
                Debug.Log("No directives found!");
                return;
            }

            if (directives[currentDirectiveIndex].subDirectives == null ||
                directives[currentDirectiveIndex].subDirectives.Count == 0)
            {
                Debug.Log("No subdirectives found!");
                return;
            }
            
            directives[currentDirectiveIndex].DirectiveComplete?.AddListener(OnDirectiveComplete);
            directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].SubDirectiveComplete?.AddListener(OnSubDirectiveComplete);
            directives[currentDirectiveIndex].Start();
            directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].Start();
            
            if (directives.Count > 0)
            {
                uiManager.AlertNewDirective(directives[currentDirectiveIndex]);
                if (directives[currentDirectiveIndex].subDirectives.Count > 0)
                {
                    uiManager.AlertNewSubDirective(directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
                }
                else
                {
                    Debug.Log("No subdirectives found!");
                }
            }
            else
            {
                Debug.Log("No directives found!");
            }
        }

        private void OnDirectiveComplete()
        {
            //Unsubscribe
            directives[currentDirectiveIndex].DirectiveComplete.RemoveListener(OnDirectiveComplete);
            
            currentDirectiveIndex++;
            
            if(currentDirectiveIndex < directives.Count)
            {
                directives[currentDirectiveIndex].DirectiveComplete?.AddListener(OnDirectiveComplete);
                directives[currentDirectiveIndex].Start();
                uiManager.AlertNewDirective(directives[currentDirectiveIndex]);

                currentSubDirectiveIndex = 0;

                if (directives[currentDirectiveIndex].subDirectives == null ||
                    directives[currentDirectiveIndex].subDirectives.Count == 0)
                {
                    Debug.Log("Directive has no subdirectives!");
                    return;
                }
                
                directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].SubDirectiveComplete.AddListener(OnSubDirectiveComplete);
                directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].Start();
                uiManager.AlertNewSubDirective(directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
            }
            else
            {
                Debug.Log("All directives complete!");
            }
        }

        private void OnSubDirectiveComplete()
        {
            if (currentDirectiveIndex >= directives.Count)
                return;
            
            //Unsubscribe
            directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].SubDirectiveComplete.RemoveListener(OnSubDirectiveComplete);
            
            currentSubDirectiveIndex++;

            if (currentSubDirectiveIndex < directives[currentDirectiveIndex].subDirectives.Count)
            {
                //Subscribe
                directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].SubDirectiveComplete?.AddListener(OnSubDirectiveComplete);
                directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex].Start();
                uiManager.AlertNewSubDirective(directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
            }
            else
            {
                currentSubDirectiveIndex = 0;
            }
        }
    }
    
    [System.Serializable]
    public class Directive
    {
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] public List<SubDirective> subDirectives = new List<SubDirective>();
        [HideInInspector] public UnityEvent DirectiveComplete = new UnityEvent();
        private int currentSubDirectiveIndex;

        public void Start()
        {
            currentSubDirectiveIndex = 0;
            subDirectives[currentSubDirectiveIndex].SubDirectiveComplete?.AddListener(OnSubDirectiveComplete);
        }

        private void OnSubDirectiveComplete()
        {
            //Unsubscribe
            subDirectives[currentSubDirectiveIndex].SubDirectiveComplete.RemoveListener(OnSubDirectiveComplete);
            
            currentSubDirectiveIndex++;

            if (currentSubDirectiveIndex < subDirectives.Count)
            {
                //Subscribe
                subDirectives[currentSubDirectiveIndex].SubDirectiveComplete?.AddListener(OnSubDirectiveComplete);
            }
            else
            {
                Debug.Log("All subdirectives complete!");
                currentSubDirectiveIndex = 0;
                DirectiveComplete?.Invoke();
            }
        }
    }
    
    [System.Serializable]
    public class SubDirective
    {
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] private MissionObject missionObject;
        [HideInInspector] public UnityEvent SubDirectiveComplete = new UnityEvent();

        public void Start()
        {
            missionObject.onScanned.AddListener(OnScanned);
        }

        private void OnScanned()
        {
            missionObject.queueAlert();
            SubDirectiveComplete?.Invoke();
            missionObject.onScanned.RemoveListener(OnScanned);
        }
    }
}
