using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Grupp14
{
    public class AlertManager : MonoBehaviour
    {
        private GameObject alertBox;
        private RectTransform alertBoxRect;

        public float directiveAlertDuration = 5f;
        public float subDirectiveAlertDuration = 5f;

        private Alert currentAlert;
        public Alert CurrentAlert => currentAlert;

        private bool fadeIn = false;
        private bool fadeOut = false;
        
        [SerializeField] private float alertYOffset = 75f;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private float fadeDuration = 0.5f;

        private float fadeTimer = 0f;
        private float startY;
        private float targetY;

        [HideInInspector] public UnityEvent onAlertStarted;
        [HideInInspector] public UnityEvent onAlertEnded;

        public Queue<Alert> alertQueue = new Queue<Alert>();

        void Start()
        {
            //Finding GameObjects
            alertBox = GameObject.Find("AlertBox");
            
            alertBoxRect = alertBox.GetComponent<RectTransform>();
            alertBoxRect.anchoredPosition = new Vector2(0, alertBoxRect.sizeDelta.y);
        }

        void Update()
        {
            if (currentAlert == null)
            {
                TryStartAlert();
            }
            else
            {
                if (fadeIn)
                {
                    fadeTimer += Time.deltaTime;
                    float progress = Mathf.Clamp01(fadeTimer / fadeDuration);
                    float curved = easeCurve.Evaluate(progress);

                    float newY = Mathf.Lerp(startY, targetY, curved);
                    alertBoxRect.anchoredPosition = new Vector2(0, newY);

                    if (progress >= 1f)
                    {
                        fadeIn = false;
                    }
                }
                else if (fadeOut)
                {
                    fadeTimer += Time.deltaTime;
                    float progress = Mathf.Clamp01(fadeTimer / fadeDuration);
                    float curved = easeCurve.Evaluate(progress);

                    float newY = Mathf.Lerp(startY, targetY * 2, curved);
                    alertBoxRect.anchoredPosition = new Vector2(0, newY);

                    if (progress >= 1f)
                    {
                        fadeOut = false;
                        currentAlert = null;
                        onAlertEnded?.Invoke();
                    }
                }
                else
                {
                    currentAlert.duration -= Time.deltaTime;
                    Debug.Log("Duration left: " + currentAlert.duration);

                    if (currentAlert.duration <= 0)
                    {
                        fadeOut = true;
                        fadeIn = false;
                        fadeTimer = 0f;

                        startY = alertBoxRect.anchoredPosition.y;
                        targetY = alertBoxRect.sizeDelta.y;
                    }
                }
            }
        }

        private void TryStartAlert()
        {
            if (alertQueue.TryDequeue(out Alert newAlert))
            {
                currentAlert = newAlert;
                onAlertStarted?.Invoke();

                fadeIn = true;
                fadeOut = false;
                fadeTimer = 0f;

                startY = alertBoxRect.anchoredPosition.y;
                targetY = -alertYOffset;
            }
        }
    }

    public class Alert
    {
        public string title, message;
        public float duration;
    }
}
