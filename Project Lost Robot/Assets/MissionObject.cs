using System;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    [RequireComponent(typeof(ScanableData))]
    public class MissionObject : MonoBehaviour
    {
        public UnityEvent onScanned;
        
        private AlertManager alertManager;

        [SerializeField] private string alertTitle;
        [SerializeField] private string alertMessage;
        [Tooltip("Seconds")] [SerializeField] private float alertDuration;
        
        private void Start()
        {
            //Subscribe to Event
            GetComponent<InteractTrigger>().scanEvent.AddListener(Scanned);
            
            alertManager = GameObject.Find("AlertManager").GetComponent<AlertManager>();
        }

        public void Scanned()
        {
            alertManager.alertQueue.Enqueue(new Alert()
            {
                title = alertTitle,
                message = alertMessage,
                duration = alertDuration
            });
        }
    }
}
