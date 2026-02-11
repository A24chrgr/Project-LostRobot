using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class InteractTrigger : MonoBehaviour
    {
        public UnityEvent interactEvent, pickUpEvent, punchEvent;
        [SerializeField] bool isAllowedAnimal = true, isAllowedRobot = true;

        void Start()
        {
            if (interactEvent == null)
                interactEvent = new UnityEvent();
            if (pickUpEvent == null)
                pickUpEvent = new UnityEvent();
            if (punchEvent == null)
                punchEvent = new UnityEvent();
        }

        public void InteractEvent(string sender)
        {
            if (sender == "Ralos" && !isAllowedRobot) { return; }
            else if (sender == "Mango" && !isAllowedAnimal) { return; }
            else if (sender != "Ralos" && sender != "Mango")
            {
                Debug.LogWarning("InteractEvent sent from " + sender + " is not a valid player");
            }
            interactEvent.Invoke();
        }

        public void PickUpEvent(string sender)
        {
            if (sender == "Ralos" && !isAllowedRobot) { return; }
            else if (sender == "Mango" && !isAllowedAnimal) { return; }
            else if (sender != "Ralos" && sender != "Mango")
            {
                Debug.LogWarning("PickUpEvent sent from " + sender + " is not a valid player");
            }
            pickUpEvent.Invoke();
        }
        public void PunchEvent(string sender)
        {
            if (sender == "Ralos" && !isAllowedRobot) { return; }
            else if (sender == "Mango" && !isAllowedAnimal) { return; }
            else if (sender != "Ralos" && sender != "Mango")
            {
                Debug.LogWarning("PunchEvent sent from " + sender + " is not a valid player");
            }
            punchEvent.Invoke();
        }
    }
}
