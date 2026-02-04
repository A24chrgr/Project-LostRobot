using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rB;
    public PlayerInput playerInput;
    private InputAction jumpAction;

    [SerializeField] float JumpForce = 12f, gravityScaleDefault = 90f, gravityScaleHover = 10f, jumpTimeDelay = 0.1f, maxJumpTime = 0.05f;
    private float gravityScaleActive, timeHoldingJump, fixedDeltaTime, deltaTime;
    [SerializeField] bool isGrounded, isOnCooldown;
    private bool isHoldingJumpButton, toggledHover, isHoverAvailable;
    [SerializeField] GameObject ballGameObject;
    [SerializeField] float hoverHeight = 0.75f, forceDownHeight = 1, blockHoveringHeight = 2;
    private PlayerMovement pM;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        pM = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions.FindAction("Jump");
        gravityScaleActive = gravityScaleDefault;
    }

    void Update()
    {
        deltaTime = Time.deltaTime;
        if (!isGrounded) rB.AddForce(new Vector3(0, -gravityScaleActive, 0), ForceMode.Acceleration); // Simulates Gravity
        if (jumpAction.WasPressedThisFrame())
        {
            isHoldingJumpButton = true;
            if (isGrounded && !isOnCooldown) timeHoldingJump = 0;
        }
        if (jumpAction.WasPressedThisFrame() && !isGrounded) { toggledHover = true; }
        if (jumpAction.WasReleasedThisFrame()) { isHoldingJumpButton = false; toggledHover = false; StartCoroutine(JumpCooldown()); }
    }
    void FixedUpdate()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        CheckGrounded();
        if (isHoldingJumpButton)
        {
            if (timeHoldingJump < maxJumpTime) // The jump force depending on hold
            {
                rB.AddForce(new Vector3(0, JumpForce, 0), ForceMode.VelocityChange);
                timeHoldingJump += fixedDeltaTime;
            }
            else if (!isGrounded && toggledHover && isHoverAvailable) // If holding trigger hover
            {
                gravityScaleActive = gravityScaleHover;
                pM.ForcedMovement(Vector3.up);
                pM.isForced = true;
            }
            else // if grounded again trigger a forced reset
            {
                isHoldingJumpButton = false;
                gravityScaleActive = gravityScaleDefault;
                if(!isOnCooldown) StartCoroutine(JumpCooldown());
                pM.isForced = false;
                toggledHover = false;
            }
        }
        else
        {
            gravityScaleActive = gravityScaleDefault;
            pM.isForced = false;
            toggledHover = false;
        }
    }
    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 15f))
        {
            if (hit.transform.CompareTag("Ground") && hit.distance <= forceDownHeight)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (hit.transform.CompareTag("Ground") && (hit.distance <= blockHoveringHeight))
            {
                isHoverAvailable = false;
            }
            else
            {
                isHoverAvailable = true;
            }
            if (isGrounded && !isOnCooldown)
            {
                transform.position = transform.position + Vector3.down * (hit.distance - hoverHeight);
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator JumpCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(jumpTimeDelay);
        isOnCooldown = false;
    }

}
