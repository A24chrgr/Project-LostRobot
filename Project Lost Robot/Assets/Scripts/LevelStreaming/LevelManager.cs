using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grupp14.LevelStreaming
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager singleton;
        private static bool busy;
        
        public LoadingScreen loadingScreen;
        
        public static void LoadLevel(string levelName)
        {
            if (busy) return;

            singleton.StartCoroutine(LoadLevelAsync(levelName));
        }

        private static IEnumerator LoadLevelAsync(string levelName)
        {
            busy = true;

            yield return singleton.loadingScreen.FadeOut();
            
            yield return SceneManager.LoadSceneAsync(levelName);
            
            yield return singleton.loadingScreen.FadeIn();
            
            busy = false;
        }

        private void Awake()
        {
            if (Singleton.TrySetSingleton(ref singleton, this))
            {
                busy = false;
            }
        }

        private void OnDestroy()
        {
            Singleton.TryUnsetSingleton(ref singleton, this);
        }
    }
}