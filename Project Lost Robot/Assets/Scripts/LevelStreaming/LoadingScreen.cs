using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Grupp14.LevelStreaming {
    public class LoadingScreen : MonoBehaviour
    {
        public float fadeDuration = 0.1f;
        public Image blackScreen;

        public IEnumerator FadeIn()
        {
            yield return Fade(Color.black, Color.clear, fadeDuration);
        }

        public IEnumerator FadeOut()
        {
            yield return Fade(Color.clear, Color.black, fadeDuration);
        }

        private void Start()
        {
            blackScreen.color = Color.clear;
        }

        private IEnumerator Fade(Color from, Color to, float duration)
        {
            for (float t = 0, lerp = 0; t < duration; t += Time.deltaTime, lerp = t / duration)
            {
                blackScreen.color = Color.Lerp(from, to, lerp);
                yield return null;
            }
            
            blackScreen.color = to;
        }
    }
}