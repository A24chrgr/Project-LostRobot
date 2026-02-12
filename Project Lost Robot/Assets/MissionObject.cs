using System;
using UnityEngine;

namespace Grupp14
{
    public class MissionObject : MonoBehaviour
    {
        private AlertManager alertManager;

        [SerializeField] private string alertTitle;
        [SerializeField] private string alertMessage;
        [Tooltip("Seconds")] [SerializeField] private float alertDuration;
        
        private void Start()
        {
            alertManager = GameObject.Find("AlertManager").GetComponent<AlertManager>();
        }

        public void onAnalyzed()
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
