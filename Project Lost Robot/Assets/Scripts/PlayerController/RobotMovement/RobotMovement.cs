using System;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    [Header("Gameplay Related")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float hoverHeightTarget;
    [SerializeField] private float hoverChangeSpeed;
    [Tooltip("Degrees/s")] [SerializeField] private float rotationSpeed;
    private bool moving;
    public bool Moving { get => moving; }
    private float hoverHeight;
    private Vector3 inputDirection;
    public Vector3 InputDirection { get => inputDirection; }
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hoverHeight = transform.position.y;
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        //Step 1. Update Hover Height,
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10f, groundLayerMask))
        {
            if (transform.position.y - info.point.y < hoverHeightTarget - 0.025f)
            {
                   hoverHeight += hoverChangeSpeed * Time.deltaTime;
            }
            else if (transform.position.y - info.point.y > hoverHeightTarget + 0.025f)
            {
                hoverHeight -= hoverChangeSpeed * Time.deltaTime;
            }
            else
            {
                hoverHeight = transform.position.y;
            }
        }
        transform.position = new Vector3(transform.position.x, hoverHeight, transform.position.z);
        
        //Step 2. Update Transform Rotation
        Vector3 currentForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight   = mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y   = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 targetDirection =
            camForward * inputDirection.y +
            camRight   * inputDirection.x;
        
        float radians = rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 newForward =
            Vector3.RotateTowards(currentForward, targetDirection, radians, 0f);
        transform.rotation = Quaternion.LookRotation(newForward, Vector3.up);
        
        //Step 3. Update Position based on transform.rotation,
        if (moving)
        {
            Vector3 moveDir = transform.forward;
            moveDir.y = 0f;
            moveDir.Normalize();

            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            moving = false;
        }
        else
        {
            moving = true;
            inputDirection = context.ReadValue<Vector2>().normalized;
        }
    }
}
