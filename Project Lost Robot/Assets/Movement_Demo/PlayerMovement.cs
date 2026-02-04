using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Smoothing of starting and ending movement | (Recommend 0.2f)")][Range(0f, 1f)][SerializeField] float movementDampening; // 0.2f
    [SerializeField] float rotationDampening;
    [SerializeField] float movementSpeed = 7f, rotationSpeed = 210f, hoverSpeed = 7f;
    private Vector3 inputDirection;

    Vector3 redirectedNormalz;
    Vector3 redirectedNormalx;
    GameObject cameraObject;

    private bool movementKeyIsHeld;
    private Rigidbody rB;
    private Vector2 speedVector;
    [NonSerialized] public bool isForced;
    [Header("Debug")]
    [SerializeField] GameObject facingBall;

    void Awake()
    {
        speedVector.x = movementSpeed;
        speedVector.y = movementSpeed;
        rB = GetComponent<Rigidbody>();
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        UpdateMovement();
        DelayedRotation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled) { movementKeyIsHeld = false; /* Gamepad.current.SetMotorSpeeds(0, 0); */ return; }
        else
        {
            // Gamepad.current.SetMotorSpeeds(0.05f, 0.1f);
            movementKeyIsHeld = true;
            inputDirection = context.ReadValue<Vector2>().normalized;

            if (inputDirection.x != 0)
            {
                speedVector.x = movementSpeed;
            }
            if (inputDirection.y != 0)
            {
                speedVector.y = movementSpeed;
            }
        }
    }
    void UpdateMovement()
    {
        if (isForced) return;
        Vector3 cameraFacingNormal = new Vector3(cameraObject.transform.forward.x, 0, cameraObject.transform.forward.z);
        redirectedNormalz = cameraFacingNormal.normalized;
        redirectedNormalx = Vector3.Cross(Vector3.up, cameraFacingNormal).normalized;


        rB.linearVelocity = Vector3.Lerp(rB.linearVelocity, (redirectedNormalx * inputDirection.x * speedVector.x) + (redirectedNormalz * inputDirection.y * speedVector.y), Mathf.Max(movementDampening, 0.001f));

        if ((inputDirection.x == 0 || !movementKeyIsHeld) && speedVector.x != 0)
        {
            speedVector.x = speedVector.x / 2;
            if (speedVector.x < 0.05f)
            {
                speedVector.x = 0;
            }
        }
        if ((inputDirection.y == 0 || !movementKeyIsHeld) && speedVector.y != 0)
        {
            speedVector.y = speedVector.y / 2;
            if (speedVector.y < 0.05f)
            {
                speedVector.y = 0;
            }
        }
    }
    public void ForcedMovement(Vector3 forcedDirection)
    {
        Vector3 cameraFacingNormal = new Vector3(cameraObject.transform.forward.x, 0, cameraObject.transform.forward.z);
        redirectedNormalz = cameraFacingNormal.normalized;
        redirectedNormalx = Vector3.Cross(Vector3.up, cameraFacingNormal).normalized;


        rB.linearVelocity = Vector3.Lerp(rB.linearVelocity, (redirectedNormalx * forcedDirection.x * movementSpeed) + (transform.forward * forcedDirection.y * hoverSpeed * 1.5f), Mathf.Max(movementDampening, 0.001f));

        if ((inputDirection.x == 0 || !movementKeyIsHeld) && speedVector.x != 0)
        {
            speedVector.x = speedVector.x / 2;
            if (speedVector.x < 0.05f)
            {
                speedVector.x = 0;
            }
        }
        if ((inputDirection.y == 0 || !movementKeyIsHeld) && speedVector.y != 0)
        {
            speedVector.y = speedVector.y / 2;
            if (speedVector.y < 0.05f)
            {
                speedVector.y = 0;
            }
        }
    }
    // private Vector3 facingDirection;
    /* void UpdateRotation()
    {
        if (movementKeyIsHeld)
        {
            facingDirection = Vector3.Lerp(facingDirection, transform.position + (redirectedNormalx * inputDirection.x + redirectedNormalz * inputDirection.y * 2), 1);
            facingBall.transform.position = facingDirection;
            transform.LookAt(facingDirection);
        }
    } */
    void DelayedRotation()
    {
        if(!movementKeyIsHeld) return;
        Vector3 currentForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        Vector3 camForward = cameraObject.transform.forward;
        Vector3 camRight = cameraObject.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 targetDirection =
            camForward * inputDirection.y +
            camRight * inputDirection.x;

        float radians = rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 newForward =
            Vector3.RotateTowards(currentForward, targetDirection, radians, 0f);
        transform.rotation = Quaternion.LookRotation(newForward, Vector3.up);
    }
}
