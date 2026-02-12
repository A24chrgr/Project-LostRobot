using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    public class PlayerPunch : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
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
            foreach (Collider col in Physics.OverlapSphere(transform.position, 1f, interactableLayer.value))
            {
                if (!col.gameObject.GetComponent<PunchableData>()) continue;
                col.gameObject.GetComponent<InteractTrigger>()?.PunchEvent(gameObject.tag);
                continue;
            }
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1, interactableLayer.value))
            {
                if (!hit.transform.GetComponent<PunchableData>()) return;
                hitObject = hit.transform.gameObject;
                hitObject.GetComponent<InteractTrigger>()?.PunchEvent(gameObject.tag);

            }
        }
    }
}
