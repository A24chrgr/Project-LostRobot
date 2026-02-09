using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderScript : MonoBehaviour
{

    [SerializeField] private string previousScene, currentScene, nextScene;
    
    
    public void LoadNextScene()
    {
        
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        
        
        
    }







}

