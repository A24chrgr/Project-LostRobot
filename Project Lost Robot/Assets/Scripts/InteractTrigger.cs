using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class InteractTrigger : MonoBehaviour
    {
        public UnityEvent interactEvent;
        [SerializeField] bool isAllowedAnimal = true, isAllowedRobot = true;

        void Start()
        {
            if (interactEvent == null)
                interactEvent = new UnityEvent();
        }

        public void TriggerEvent(string sender)
        {
            Debug.Log(sender);
            if (sender == "Ralos" && !isAllowedRobot) { return; }
            else if (sender == "Mango" && !isAllowedAnimal) { return; }
            else if (sender != "Ralos" && sender != "Mango" )
            {
                Debug.LogWarning("Interacttrigger sent from " + sender + " is not a valid player");
            }
            interactEvent.Invoke();
        }
    }
}
