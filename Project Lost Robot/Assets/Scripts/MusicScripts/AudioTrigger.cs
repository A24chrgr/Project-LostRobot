using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Serialization;
using STOP_MODE = FMOD.Studio.STOP_MODE;

[System.Serializable]

// Klassen AudioSetting innehåller dem saker som skall synas i inspektorn och som kan konfigureras av utvecklaren.
public class AudioSetting
{ 
    public bool destroyAfterUse = true;
    
    [HideInInspector] 
    public StudioEventEmitter emitter;
    public GameObject GameObject;
    public EventInstance mainMusicEventInstance;
    public EventReference mainMusicEventReference;
    public AudioAction audioAction = AudioAction.None;
    public string parameter = "";
    public float targetValue;
}

// Alla actions har tre varianter av sig förutom "None". Dessa actions kan starta, stoppa eller ändra en parameter.
public enum AudioAction
{
    None,
    Play1,
    Stop1,
    SetParameter1,
    Play2,
    Stop2,
    SetParameter2,
    Play3,
    Stop3,
    SetParameter3,
}

// Själva scriptet för triggern. Kommunicerar med MusicManager för att ändra eventinstanser och eventreferenser, detta gäller även om det skall startas ett Fmod-event eller ändra en parameter.
public class AudioTrigger : MonoBehaviour
{
    
    [NonReorderable] public AudioSetting[] audioSettings;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.instance.SetAudioSettings(audioSettings);
            Debug.Log("AudioTrigger enter");
        }
        else
        {
            Debug.Log("It dont work:(");
        }
        
        
    }
}