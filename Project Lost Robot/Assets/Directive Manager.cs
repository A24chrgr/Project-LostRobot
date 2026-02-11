using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class DirectiveManager : MonoBehaviour
    {
        public int currentDirectiveIndex;
        public int currentSubDirectiveIndex;
        public List<Directive> directives;
        public UnityEvent DirectiveComplete = new UnityEvent();
        public UnityEvent SubDirectiveComplete = new UnityEvent();
        
        void Start()
        {
            
        }
        
        void Update()
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
    }
}
