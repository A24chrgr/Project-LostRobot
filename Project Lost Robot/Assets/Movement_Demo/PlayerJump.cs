using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rB;
    public PlayerInput playerInput;
    private InputAction jumpAction;

    [SerializeField] float JumpForce = 12f, gravityScaleDefault = 90f, gravityScaleHover = 10f, jumpTimeDelay = 0.1f, maxJumpTime = 0.05f;
    private float gravityScaleActive, timeHoldingJump;
    [SerializeField] bool isGrounded, isOnCooldown;
    private bool isHoldingJumpButton;
    [SerializeField] GameObject ballGameObject;
    [SerializeField] float hoverHeight = 0.75f;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions.FindAction("Jump");
        gravityScaleActive = gravityScaleDefault;
    }

    void Update()
    {
        if (!isGrounded) rB.AddForce(new Vector3(0, -gravityScaleActive, 0), ForceMode.Acceleration); // Simulates Gravity
        if (isGrounded && !isOnCooldown && jumpAction.WasPressedThisFrame()) { isHoldingJumpButton = true; timeHoldingJump = 0; }
        if (jumpAction.WasReleasedThisFrame()) { isHoldingJumpButton = false; StartCoroutine(JumpCooldown()); }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        if (isHoldingJumpButton)
        {
            if (timeHoldingJump < maxJumpTime)
            {
                rB.AddForce(new Vector3(0, JumpForce, 0), ForceMode.VelocityChange);
                timeHoldingJump += Time.deltaTime;
            }
            else if (!isGrounded)
            {
                gravityScaleActive = gravityScaleHover;
            }
            else
            {
                isHoldingJumpButton = false;
                gravityScaleActive = gravityScaleDefault;
                StartCoroutine(JumpCooldown());
            }
        }
        else
        {
            gravityScaleActive = gravityScaleDefault;
        }
    }
    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            ballGameObject.transform.position = transform.position + Vector3.down * hit.distance;
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
    public IEnumerator TestJump(InputAction.CallbackContext context)
    {
        yield return new WaitForSeconds(jumpTimeDelay);
    }



    IEnumerator JumpCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(jumpTimeDelay);
        isOnCooldown = false;
    }

}
