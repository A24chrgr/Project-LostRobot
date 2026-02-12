using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grupp14
{
    public class PlayerScan : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        [NonSerialized] public PlayerInput playerInput;
        private InputAction scanAction;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            scanAction = playerInput.actions.FindAction("ScanObject");
        }
        void Update()
        {
            if (scanAction.WasPressedThisFrame())
            {
                Scan();
            }
            if (scanAction.WasReleasedThisFrame())
            {
            }
        }

        void Scan()
        {
            GameObject hitObject;
            RaycastHit hit;
            foreach (Collider col in Physics.OverlapSphere(transform.position, 1f, interactableLayer.value))
            {
                if (!col.gameObject.GetComponent<ScanableData>()) continue;
                col.gameObject.GetComponent<InteractTrigger>()?.ScanEvent(gameObject.tag);
                continue;
            }
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1, interactableLayer.value))
            {
                if (!hit.transform.GetComponent<ScanableData>()) return;
                hitObject = hit.transform.gameObject;
                hitObject.GetComponent<InteractTrigger>()?.ScanEvent(gameObject.tag);
            }
        }
    }
}
