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
                    if (hit.transform.gameObject.GetComponent<PickUpData>()) PickUp(hit);
                    if (hit.transform.gameObject.GetComponent<InteractTrigger>()) hit.transform.gameObject.GetComponent<InteractTrigger>()?.TriggerEvent();

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
            heldObject.transform.parent = holdingPoint.transform;
            heldObject.transform.position = holdingPoint.transform.position;
            heldObject.GetComponent<Collider>().enabled = false;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
