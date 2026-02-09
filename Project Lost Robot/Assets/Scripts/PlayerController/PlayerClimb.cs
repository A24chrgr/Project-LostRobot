using UnityEngine;
using UnityEngine.InputSystem;

namespace Grupp14
{
    public class PlayerClimb : MonoBehaviour
    {
        public bool isInClimbZone;
        public bool isClimbing;
        public PlayerInput playerInput;
        private InputAction climbAction;
        private PlayerMovement pM;
        private PlayerJump pJ;
        private bool hasClimb;
        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            climbAction = playerInput.actions.FindAction("Climb");
            pM = GetComponent<PlayerMovement>();
            pJ = GetComponent<PlayerJump>();
        }

        void Update()
        {
            if (isInClimbZone)
            {
                if (climbAction.WasPressedThisFrame() && isClimbing) { EndClimbing(); return; }
                if (climbAction.WasPressedThisFrame() && !isClimbing) { StartClimbing(); }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Finish")) // ClimbZone
            {
                isInClimbZone = true;
            }
            if (isInClimbZone && other.gameObject.CompareTag("Respawn")) // ExitZone
            {
                isInClimbZone = false;
                EndClimbing();
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                transform.position = transform.position + transform.forward * 2 + transform.up * 2;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Finish")) // ClimbZone
            {
                isInClimbZone = false;
                hasClimb = false;
                EndClimbing();
            }
        }
        void StartClimbing()
        {
            isClimbing = true;
            hasClimb = true;
            pM.isClimbing = true;
            pJ.isClimbing = true;
        }

        void EndClimbing()
        {
            isClimbing = false;
            pM.isClimbing = false;
            pJ.isClimbing = false;
        }
    }
}
