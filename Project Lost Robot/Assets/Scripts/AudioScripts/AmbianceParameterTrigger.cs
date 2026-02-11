using System;
using UnityEngine;
using static Grupp14.AmbianceParameterTrigger;
using static UnityEditor.FilePathAttribute;

namespace Grupp14
{
    public class AmbianceParameterTrigger : MonoBehaviour
    {
        public enum Action // possible actions configurable in inspector
        {
            Play,
            Stop,
            SetParameter
        }

        public struct Parameters
        {
            public string parameterName;
            public float parameterValue;
        }
        //[Header("Parameters to configure: AirborneAnimal, BranchCreak, InsectChittering, ShadowCar, ShadowSpeak, TreeSway, WindIntensity")]
        [Serializable]
        public struct AudioSettings // possible settings to configure in inspector
        {
            public Action action;
            public Parameters parameterSettings;
        }

        public AudioSettings[] triggerEnterSettings; // manually configure in inspector the settings

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach(AudioSettings i in triggerEnterSettings)
                {
                    switch (i.action)
                    {
                        case Action.Play:
                            AmbianceManager.instance.PlayAudio();
                            break;
                        case Action.Stop:
                            AmbianceManager.instance.StopAudio();
                            break;
                        /*case Action.SetParameter:
                            AmbianceManager.instance.SetParameter();
                            break;*/
                    }
                }
            }
        }
    }
}
