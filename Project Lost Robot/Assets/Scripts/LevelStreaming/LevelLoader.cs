using UnityEngine;

namespace Grupp14.LevelStreaming
{
    public class LevelLoader : MonoBehaviour
    {
        public string levelName;
        
        public void LoadLevel()
        {
            LevelManager.LoadLevel(levelName);
        }
    }
}