using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Grupp14
{
    public class UIManager : MonoBehaviour
    {
        private int currentDirectiveIndex, currentSubDirectiveIndex;
        
        //Managers
        private DirectiveManager directiveManager;
        private AlertManager alertManager;
        
        //Canvas GameObjects
        private TextMeshProUGUI AlertBoxTitle;
        private TextMeshProUGUI AlertBoxDescription;
        private TextMeshProUGUI DirectiveName;
        private TextMeshProUGUI DirectiveDescription;
        private TextMeshProUGUI SubDirectiveName;
        private TextMeshProUGUI SubDirectiveDescription;

        private void Start()
        {
            //Finding GameObjects
            AlertBoxTitle = GameObject.Find("AlertBoxTitle").GetComponent<TextMeshProUGUI>();
            AlertBoxDescription = GameObject.Find("AlertBoxDescription").GetComponent<TextMeshProUGUI>();
            DirectiveName = GameObject.Find("DirectiveName").GetComponent<TextMeshProUGUI>();
            DirectiveDescription = GameObject.Find("DirectiveDescription").GetComponent<TextMeshProUGUI>();
            SubDirectiveName = GameObject.Find("SubDirectiveName").GetComponent<TextMeshProUGUI>();
            SubDirectiveDescription = GameObject.Find("SubDirectiveDescription").GetComponent<TextMeshProUGUI>();
            
            //Finding Managers
            alertManager = GameObject.Find("AlertManager").GetComponent<AlertManager>();
            directiveManager = GameObject.Find("DirectiveManager").GetComponent<DirectiveManager>();
            
            //Subscribing to Events
            alertManager.onAlertStarted.AddListener(OnAlertStarted);
            alertManager.onAlertEnded.AddListener(OnAlertEnded);
            directiveManager.DirectiveComplete.AddListener(OnDirectiveComplete);
            directiveManager.SubDirectiveComplete.AddListener(OnSubDirectiveComplete);
            
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

        private void OnAlertStarted()
        {
            AlertBoxTitle.text = alertManager.CurrentAlert.title;
            AlertBoxDescription.text = alertManager.CurrentAlert.message;
        }
        
        private void OnAlertEnded()
        {
            AlertBoxTitle.text = "";
            AlertBoxDescription.text = "";
        }
        
        private void OnDirectiveComplete()
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

        private void OnSubDirectiveComplete()
        {
            currentSubDirectiveIndex++;
            if (currentSubDirectiveIndex < directiveManager.directives[currentDirectiveIndex].subDirectives.Count)
            {
                NewSubDirective(directiveManager.directives[currentDirectiveIndex].subDirectives[currentSubDirectiveIndex]);
            }
        }
    }
}
