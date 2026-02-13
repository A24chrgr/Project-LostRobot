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

        private void Awake()
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
        }

        private void Start()
        {
            //Subscribing to Events
            alertManager.onAlertStarted.AddListener(OnAlertStarted);
            alertManager.onAlertEnded.AddListener(OnAlertEnded);
            directiveManager.DirectiveComplete.AddListener(OnDirectiveComplete);
            directiveManager.SubDirectiveComplete.AddListener(OnSubDirectiveComplete);
            
            currentDirectiveIndex = directiveManager.currentDirectiveIndex;
            currentSubDirectiveIndex = directiveManager.currentSubDirectiveIndex;
        }

        public void AlertNewDirective(Directive directive)
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

        public void AlertNewSubDirective(SubDirective subDirective)
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
            //Handled in Directive Manager instead
        }

        private void OnSubDirectiveComplete()
        {
            //Handled in Directive Manager instead
        }
    }
}
