using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    public class PlayerPunch : MonoBehaviour
    {
        public PlayerInput playerInput;
        private InputAction punchAction;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            punchAction = playerInput.actions.FindAction("Punch");
        }
        void Update()
        {
            if (punchAction.WasPressedThisFrame())
            {
                Punch();
            }
            if (punchAction.WasReleasedThisFrame())
            {
                
            }
        }

        void Punch()
        {
            GameObject hitObject;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1))
            {
                if (!hit.transform.CompareTag("Punchable")) return;
                hitObject = hit.transform.gameObject;
                hitObject.GetComponent<InteractTrigger>()?.TriggerEvent();
            }
        }
    }
}
