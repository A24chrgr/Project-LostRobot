using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class DirectiveManager : MonoBehaviour
    {
        public List<Directive> directives;
        
        [HideInInspector] public int currentDirectiveIndex;
        [HideInInspector] public int currentSubDirectiveIndex;
        
        [HideInInspector] public UnityEvent DirectiveComplete = new UnityEvent();
        [HideInInspector] public UnityEvent SubDirectiveComplete = new UnityEvent();
        
        void Start()
        {
            
        }
    }
    
    [System.Serializable]
    public class Directive
    {
        [SerializeField] public string name;
        [SerializeField] public string description;
        
        [SerializeField] public List<SubDirective> subDirectives = new List<SubDirective>();
    }
    
    [System.Serializable]
    public class SubDirective
    {
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] private MissionObject missionObject;
    }
}
