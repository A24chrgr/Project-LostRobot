using UnityEngine;
using System.Collections.Generic;

namespace Grupp14
{
    public class AlertManager : MonoBehaviour
    {
        [SerializeField] private GameObject alertBox;
        private RectTransform alertBoxRect;
        public float directiveAlertDuration = 5f;
        public float subDirectiveAlertDuration = 5f;

        [SerializeField] private float alertYOffset;
        
        public Queue<Alert> alertQueue = new Queue<Alert>();
        
        void Start()
        {
            alertBoxRect = alertBox.GetComponent<RectTransform>();
            alertBoxRect.anchoredPosition = new Vector2(0, alertBoxRect.sizeDelta.y);
        }
        
        void Update()
        {
        
        }
    }
    
    public class Alert
    {
        public string title, message;
        public float duration;
    }
}
