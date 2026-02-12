using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        [NonSerialized] public bool isHoldingObject, isHoldingMango;
        /* [NonSerialized] */ public GameObject heldObject;
        [SerializeField] GameObject itemHoldingPoint;
        [SerializeField] GameObject playerHoldingPoint;
        [SerializeField] bool allowMangoPickup;

        [NonSerialized] public PlayerInput playerInput;
        private InputAction interactAction;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            interactAction = playerInput.actions.FindAction("PickUp");
        }
        void Update()
        {
            RaycastHit hit;
            if (interactAction.WasPressedThisFrame())
            {
                if (isHoldingMango)
                {
                    DropMango();
                    return;
                }
                else if (isHoldingObject)
                {
                    Drop();
                    return;
                }
                if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1, interactableLayer.value))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (allowMangoPickup && hit.transform.gameObject.tag == "Mango") { MangoPickUp(hit); return; }
                    CheckForPickUp(hitObject);
                }
                foreach (Collider col in Physics.OverlapSphere(transform.position, 1f, interactableLayer.value))
                {
                    CheckForPickUp(col.gameObject);
                }
            }
        }

        private void CheckForPickUp(GameObject hitObject)
        {
            if (hitObject.GetComponent<PickUpData>())
            {
                if (hitObject.GetComponent<PickUpData>().CheckIfAllowed(gameObject.tag))
                {
                    PickUp(hitObject);
                }
            }
            else
            {
                if (hitObject.GetComponent<InteractTrigger>()) hitObject.GetComponent<InteractTrigger>()?.InteractEvent(gameObject.tag);
            }
        }

        void Drop()
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
            isHoldingObject = false;
        }
        void PickUp(GameObject currentObject)
        {
            heldObject = currentObject;
            isHoldingObject = true;
            currentObject.transform.parent = itemHoldingPoint.transform;
            currentObject.transform.position = itemHoldingPoint.transform.position;
            currentObject.GetComponent<Collider>().enabled = false;
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            if (currentObject.GetComponent<InteractTrigger>()) currentObject.GetComponent<InteractTrigger>()?.PickUpEvent(gameObject.tag);

        }

        void MangoPickUp(RaycastHit hit)
        {
            heldObject = hit.transform.gameObject;
            heldObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            heldObject.GetComponent<MangoHeld>().isHeld = true;
            heldObject.GetComponent<MangoHeld>().ppU = this;
            isHoldingMango = true;
            heldObject.transform.parent = playerHoldingPoint.transform;
            heldObject.transform.position = playerHoldingPoint.transform.position;
            heldObject.GetComponent<PlayerMovement>().enabled = false;
            heldObject.GetComponent<PlayerJump>().enabled = false;
            heldObject.GetComponent<PlayerClimb>().enabled = false;
            heldObject.GetComponent<PlayerPickUp>().Drop();
            heldObject.GetComponent<PlayerPickUp>().enabled = false;
        }
        public void DropMango()
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<PlayerMovement>().enabled = true;
            heldObject.GetComponent<PlayerJump>().enabled = true;
            heldObject.GetComponent<PlayerClimb>().enabled = true;
            heldObject.GetComponent<PlayerPickUp>().enabled = true;
            heldObject.GetComponent<MangoHeld>().isHeld = false;
            heldObject.GetComponent<MangoHeld>().ppU = null;
            heldObject = null;
            isHoldingMango = false;
        }
    }
}
