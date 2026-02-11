using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    public class PlayerPickUp : MonoBehaviour
    {
        [NonSerialized] public bool isHoldingObject, isHoldingMango;
        [NonSerialized] public GameObject heldObject;
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
                }
                else if (isHoldingObject)
                {
                    Drop();
                }
                else if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (allowMangoPickup && hit.transform.gameObject.tag == "Mango") { MangoPickUp(hit); return; }
                    if (hitObject.GetComponent<PickUpData>())
                    {
                        if (hitObject.GetComponent<PickUpData>().CheckIfAllowed(gameObject.tag))
                        {
                            PickUp(hit);
                        }
                    }
                    else
                    {
                        if (hitObject.GetComponent<InteractTrigger>()) hitObject.GetComponent<InteractTrigger>()?.InteractEvent(gameObject.tag);
                    }
                    hitObject = null;
                }
            }
            if (interactAction.WasReleasedThisFrame())
            {
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
        void PickUp(RaycastHit hit)
        {
            heldObject = hit.transform.gameObject;
            isHoldingObject = true;
            heldObject.transform.parent = itemHoldingPoint.transform;
            heldObject.transform.position = itemHoldingPoint.transform.position;
            heldObject.GetComponent<Collider>().enabled = false;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            if (heldObject.GetComponent<InteractTrigger>()) heldObject.GetComponent<InteractTrigger>()?.PickUpEvent(gameObject.tag);

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
