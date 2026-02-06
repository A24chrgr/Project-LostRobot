using UnityEngine;
using UnityEngine.Events;

namespace Grupp14
{
    public class InteractTrigger : MonoBehaviour
    {
        public UnityEvent interactEvent;

        void Start()
        {
            if (interactEvent == null)
                interactEvent = new UnityEvent();
        }

        public void TriggerEvent()
        {
            interactEvent.Invoke();
        }
    }
}
