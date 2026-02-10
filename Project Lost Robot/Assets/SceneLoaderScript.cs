using System.Collections;
using System.Collections.Generic;
using Grupp14.LevelStreaming;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderScript : MonoBehaviour
{

    [SerializeField] private string previousScene, currentScene, nextScene;
    
    
    
    public void LoadPreviousScene()
    {
            
        LevelManager.LoadLevel(previousScene);
            
    }
    
    
    public void LoadNextScene()
    {
        
        LevelManager.LoadLevel(nextScene);
        
    }






}

