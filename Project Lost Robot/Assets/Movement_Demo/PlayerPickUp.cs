using UnityEngine;
using UnityEngine.InputSystem;
namespace Grupp14
{
    public class PlayerPickUp : MonoBehaviour
    {
        private bool isHoldingObject;
        private GameObject heldObject;
        [SerializeField] GameObject holdingPoint;

        public PlayerInput playerInput;
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
                if (isHoldingObject)
                {
                    Drop();
                }
                else if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1))
                {
                    string tag = hit.transform.tag;
                    if (tag == "PickUpObject") PickUp(hit);
                    if (tag == "Mango") TriggerInteract(hit);

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
            TriggerInteract(hit);

            heldObject = hit.transform.gameObject;
            isHoldingObject = true;
            heldObject.transform.parent = holdingPoint.transform;
            heldObject.transform.position = holdingPoint.transform.position;
            heldObject.GetComponent<Collider>().enabled = false;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;

        }

        void TriggerInteract(RaycastHit hit)
        {
            hit.transform.gameObject.GetComponent<InteractTrigger>()?.TriggerEvent();

        }
    }
}
