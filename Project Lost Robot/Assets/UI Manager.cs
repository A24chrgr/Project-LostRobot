using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Grupp14
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private DirectiveManager directiveManager;
        private int currentDirectiveIndex, currentSubDirectiveIndex;
        
        [SerializeField] private AlertManager alertManager;
        
        //Canvas GameObjects
        [SerializeField] private TextMeshProUGUI AlertBoxTitle;
        [SerializeField] private TextMeshProUGUI AlertBoxDescription;
        [SerializeField] private TextMeshProUGUI DirectiveName;
        [SerializeField] private TextMeshProUGUI DirectiveDescription;
        [SerializeField] private TextMeshProUGUI SubDirectiveName;
        [SerializeField] private TextMeshProUGUI SubDirectiveDescription;

        private void Start()
        {
            currentDirectiveIndex = directiveManager.currentDirectiveIndex;
            currentSubDirectiveIndex = directiveManager.currentSubDirectiveIndex;
            
            if (directiveManager.directives.Count > 0)
            {
                NewDirective(directiveManager.directives[currentDirectiveIndex]);
                if (directiveManager.directives[currentDirectiveIndex].subDirectives.Count > 0)
                {
                    NewSubDirective(directiveManager.directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
                } 
            }
        }

        private void NewDirective(Directive directive)
        {
            DirectiveName.text = directive.name;
            DirectiveDescription.text = directive.description;
            alertManager.alertQueue.Enqueue(new Alert
            {
                title = $"New Directive: {directive.name}", 
                message = directive.description,
                duration = alertManager.directiveAlertDuration
            });
        }

        private void NewSubDirective(SubDirective subDirective)
        {
            SubDirectiveName.text = subDirective.name;
            SubDirectiveDescription.text = subDirective.description;
            alertManager.alertQueue.Enqueue(new Alert
            {
                title = $"New Sub-Directive: {subDirective.name}", 
                message = subDirective.description,
                duration = alertManager.subDirectiveAlertDuration
            });
        }
        
        public void OnDirectiveComplete()
        {
            currentDirectiveIndex++;
            currentSubDirectiveIndex = 0;
            if (currentDirectiveIndex < directiveManager.directives.Count)
            {
                NewDirective(directiveManager.directives[currentDirectiveIndex]);
                if (directiveManager.directives[currentDirectiveIndex].subDirectives.Count > 0)
                {
                    NewSubDirective(directiveManager.directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
                }
            }
        }

        public void OnSubDirectiveComplete()
        {
            currentSubDirectiveIndex++;
            if (currentSubDirectiveIndex < directiveManager.directives[currentDirectiveIndex].subDirectives.Count)
            {
                NewSubDirective(directiveManager.directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
            }
        }
    }
}
