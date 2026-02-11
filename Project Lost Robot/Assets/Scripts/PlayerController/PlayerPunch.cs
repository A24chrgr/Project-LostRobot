using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    [RequireComponent(typeof(InteractTrigger))]
    public class PlayerPunch : MonoBehaviour
    {
        [NonSerialized] public PlayerInput playerInput;
        private InputAction punchAction;
        private PlayerPickUp ppU;

        void Awake()
        {
            ppU = GetComponent<PlayerPickUp>();
            playerInput = GetComponent<PlayerInput>();
            punchAction = playerInput.actions.FindAction("Punch");
        }
        void Update()
        {
            if (ppU.isHoldingObject || ppU.isHoldingMango) return;
            if (punchAction.WasPressedThisFrame())
            {
            }
            if (punchAction.WasReleasedThisFrame())
            {
                Punch();
            }
        }

        void Punch()
        {
            GameObject hitObject;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1))
            {
                if (!hit.transform.GetComponent<PunchableData>()) return;
                hitObject = hit.transform.gameObject;
                hitObject.GetComponent<InteractTrigger>()?.PunchEvent(gameObject.tag);
            }
        }
    }
}
