using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

namespace Grupp14
{
    public class AmbianceManager : MonoBehaviour
    {
        public static AmbianceManager instance { get; private set; }

        public GameObject Robot;
        [SerializeField] private StudioEventEmitter AmbianceEmitter;
        void Awake()
        {
            // If there already is an instance of an ambiance manager, it will destroy itself. If there is no other ambiance manager this becomes the ambiance manager
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this); // makes sure this ambiancemanager is not destroyed when loading a new scene
        }

        void FixedUpdate()
        {
            transform.position = Robot.transform.position;
        }

        public void PlayAudio()
        {
            if (!AmbianceEmitter.IsActive) // makes sure the emitter isnt playing before executing PlayAudio()
            {
                AmbianceEmitter.Play();
            }
            else
            {
                AmbianceEmitter.Stop();
            }
        }

        public void StopAudio()
        {
            if (AmbianceEmitter.IsActive) // makes sure the emitter isnt playing before executing StopAudio()
            {
                AmbianceEmitter.Stop();
            }
        }

        public void SetParameter(string label, float value)
        {
            if (AmbianceEmitter.IsActive)
            {
                AmbianceEmitter.SetParameter(label, value);
            }
        }
    }
}
