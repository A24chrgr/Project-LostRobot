using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grupp14
{
    public class PlayerClimb : MonoBehaviour
    {
        public bool isInClimbZone, isInExitZone;
        public bool isClimbing;
        public PlayerInput playerInput;
        private InputAction climbAction;
        private PlayerMovement pM;
        private PlayerJump pJ;
        private Rigidbody rB;
        private RaycastHit hit;
        private ClimbData currentWall;
        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            climbAction = playerInput.actions.FindAction("Climb");
            pM = GetComponent<PlayerMovement>();
            pJ = GetComponent<PlayerJump>();
            rB = GetComponent<Rigidbody>();
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
            if (other.GetComponent<ClimbData>() != null) // ClimbZone
            {
                if (!isInExitZone) isInClimbZone = true;
                currentWall = other.GetComponent<ClimbData>();
                if (isClimbing) transform.rotation = Quaternion.LookRotation(Vector3.up, currentWall.normal);
            }
            if (other.GetComponent<EndClimbData>() != null) // ExitZone
            {
                isInExitZone = true;
                if (isInClimbZone)
                {
                    isInClimbZone = false;
                    transform.rotation = Quaternion.LookRotation(-currentWall.normal, Vector3.up);
                    transform.position = transform.position + -currentWall.normal * 2 + transform.up * 2;
                    EndClimbing();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<ClimbData>() != null) // ClimbZone
            {
                isInClimbZone = false;
                transform.rotation = Quaternion.LookRotation(-currentWall.normal, Vector3.up);
                EndClimbing();
            }
            if (other.GetComponent<EndClimbData>() != null) // ExitZone
            {
                isInExitZone = false;
            }
        }
        void StartClimbing()
        {
            rB.linearVelocity = Vector3.zero;
            isClimbing = true;
            pM.isClimbing = true;
            pJ.enabled = false;

            Vector3 pos = currentWall.GetComponent<BoxCollider>().center + currentWall.transform.parent.position;
            pos = new Vector3(pos.x, transform.position.y, pos.z);
            if (Physics.Raycast(pos, -currentWall.normal, out hit, 15f))
            {
                Debug.Log(hit.transform.name);
                transform.position = pos + -currentWall.normal * (hit.distance - pJ.playerHoverAboveGroundHeight);
            }
        }

        void EndClimbing()
        {
            currentWall = null;
            isClimbing = false;
            pM.isClimbing = false;
            pJ.enabled = true;
        }
    }
}
