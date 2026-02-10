using UnityEngine;
using UnityEngine.InputSystem;

namespace Grupp14
{
    public class PlayerThrow : MonoBehaviour
    {
        public PlayerInput playerInput;
        private InputAction throwAction;
        private PlayerPickUp ppU;
        private GameObject item;
        [SerializeField] float throwForce = 12;

        void Awake()
        {
            ppU = GetComponent<PlayerPickUp>();
            playerInput = GetComponent<PlayerInput>();
            throwAction = playerInput.actions.FindAction("Punch");
        }
        void Update()
        {
            if (throwAction.WasPressedThisFrame() && ppU.isHoldingMango)
            {
                Throw();
            }
        }

        void Throw()
        {
            item = ppU.heldObject;
            Rigidbody rB = item.GetComponent<Rigidbody>();
            ppU.DropMango();
            rB.AddForce(new Vector3(0, throwForce, 0), ForceMode.VelocityChange);
            item = null;
        }
    }
}
