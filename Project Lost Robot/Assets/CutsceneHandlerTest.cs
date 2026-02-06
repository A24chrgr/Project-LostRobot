using System;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneHandlerTest : MonoBehaviour
{
    
    [SerializeField] private CinemachineCamera ralosCloseUp, mangoCloseUp, midPointFocus, fruitCloseUpTemp;

    public CinemachineCamera currentCam;   //otherCloseUp;
    
    

    private GameObject fruitObject, ralos, mango;

    private float mangoSpeed;

    private Vector3 tpSpot1, tpSpot2;
    
    public void OpeningCinematicStart()
    {
        ralosCloseUp.gameObject.SetActive(true);
    }

    public void MeetingCinematicStart()
    {
        mangoCloseUp.gameObject.SetActive(true);
    }
    
    public void FruitCatchCinematic()
    {
      //why this no work?:
        //  otherCloseUp = GameObject.FindWithTag("CinematicFocusOther");
        
        fruitCloseUpTemp.gameObject.SetActive(true);

        currentCam = fruitCloseUpTemp;
        
        fruitObject = GameObject.FindGameObjectWithTag("Fruit");
        
        ralos = GameObject.FindGameObjectWithTag("Ralos");
        
        mango = GameObject.FindGameObjectWithTag("Mango");
        
        tpSpot1 = GameObject.FindGameObjectWithTag("TeleportSpot").GetComponent<Transform>().position;
        
        tpSpot2 = GameObject.FindGameObjectWithTag("TeleportSpot2temp").GetComponent<Transform>().position;
        
        ralos.transform.position = tpSpot1;
        
        //tempkod nedan--------------
        fruitObject.transform.position = tpSpot2;
        //tempkod^-----------
        
        
        //mangoSpeed = mango.GetComponent<PlayerMovement>().movementSpeed; ????
        //Eller nåt annat sätt att enkelt disable inputs
        
        //play animation of fruit falling
        
        
        
    }
    
    public void MangoDirectiveCinematicStart()
    {
        midPointFocus.gameObject.SetActive(true);
    }
    
    public void StrangleCinematicStart()
    {
        ralosCloseUp.gameObject.SetActive(true);
        
    }
    
    
    public void EndCinematic()
    {
        currentCam.gameObject.SetActive(false);
    }
    
    
    
}
