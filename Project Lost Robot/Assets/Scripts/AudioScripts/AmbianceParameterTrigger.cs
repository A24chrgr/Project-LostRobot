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

        [Tooltip("Parameters to configure: AirborneAnimal, BranchCreak, InsectChittering, ShadowCar, ShadowSpeak, TreeSway, WindIntensity. All use float values between 0.0f - 1.0f")]
        [Serializable]
        public struct AudioSettings // possible settings to configure in inspector
        {
            public Action action;
            public string parameterName;
            public float parameterValue;
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
                        case Action.SetParameter:
                            AmbianceManager.instance.SetParameter(i.parameterName, i.parameterValue);
                            break;
                    }
                }
            }
        }
    }
}
