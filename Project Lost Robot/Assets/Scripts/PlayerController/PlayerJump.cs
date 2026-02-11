using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private Rigidbody rB;
    [NonSerialized] public PlayerInput playerInput;
    private InputAction jumpAction;
    [SerializeField] private LayerMask terrainLayer;
    [Header("Jump/Hover")]
    [SerializeField] private float JumpForce = 12f;
    [SerializeField] private float jumpTimeDelay = 0.1f;
    [SerializeField] private float maxJumpTime = 0.05f;
    [SerializeField] private float maximumFallingForce = -9f;
    [SerializeField] private float airHoveringForce = -3f;
    [Tooltip("The height the player is set to when grounded")][SerializeField] public float playerHoverAboveGroundHeight = 0.75f;
    [Tooltip("The height the player is considered grounded")][SerializeField] private float forceDownHeight = 1;
    [Tooltip("The lowest height from the ground the player can still hover")][SerializeField] private float lowestHoveringHeight = 2;


    private float gravityScaleDefault = 90f, timeHoldingJump, fixedDeltaTime;
    private bool isGrounded, isOnCooldown;
    private bool isHoldingJumpButton, toggledHover, isHoverAvailable;
    private PlayerMovement pM;
    [Header("Debug")]
    [SerializeField] private int frameRate = 120;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        pM = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions.FindAction("Jump");
        QualitySettings.vSyncCount = 0;

    }

    void Update()
    {
        Application.targetFrameRate = frameRate;
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
        pM.isForced = false;
        fixedDeltaTime = Time.fixedDeltaTime;
        if (!isGrounded && !isHoldingJumpButton) Gravity(gravityScaleDefault); // Simulates Gravity
        CheckGrounded();
        if (isHoldingJumpButton)
        {
            if (timeHoldingJump < maxJumpTime) // The jump force depending on hold
            {
                rB.AddForce(new Vector3(0, JumpForce, 0), ForceMode.VelocityChange);
                timeHoldingJump += fixedDeltaTime;
            }
            else if (!toggledHover && timeHoldingJump >= maxJumpTime && !isGrounded)
            {
                Gravity(gravityScaleDefault);
            }
            else if (!isGrounded && toggledHover && isHoverAvailable) // If holding trigger hover
            {
                GravityHover();
                pM.ForcedMovement(Vector3.up);
                pM.isForced = true;
            }
        }
    }

    private void Gravity(float gravityScale)
    {
        if (rB.linearVelocity.y > maximumFallingForce) rB.AddForce(new Vector3(0, -gravityScale, 0), ForceMode.Acceleration);
    }
    private void GravityHover()
    {
        if (rB.linearVelocity.y != airHoveringForce) rB.AddForce(new Vector3(0, airHoveringForce - rB.linearVelocity.y, 0), ForceMode.VelocityChange);
    }
    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15f, terrainLayer.value))
        {
            if (hit.distance <= forceDownHeight)
            {
                Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            CheckIfHoverIsAvailable(hit);
            LockToGround(hit);
        }
        else
        {
            isGrounded = false;
        }
    }
    private void CheckIfHoverIsAvailable(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Ground") && (hit.distance <= lowestHoveringHeight))
        {
            isHoverAvailable = false;
            toggledHover = false;
        }
        else
        {
            isHoverAvailable = true;
        }
    }
    private void LockToGround(RaycastHit hit)
    {
        Vector3 downVector = Vector3.down;
        // if(hit.distance <= forceDownHeight) downVector = -hit.normal.normalized;
        if (rB.linearVelocity.y == 0 && isGrounded && !isOnCooldown)
        {
            transform.position = transform.position + downVector * (hit.distance - playerHoverAboveGroundHeight);
            rB.AddForce(new Vector3(0, -rB.linearVelocity.y, 0), ForceMode.VelocityChange);
        }
    }
    private IEnumerator JumpCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(jumpTimeDelay);
        isOnCooldown = false;
    }

}
