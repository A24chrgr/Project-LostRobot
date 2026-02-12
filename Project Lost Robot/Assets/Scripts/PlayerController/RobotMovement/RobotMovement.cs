using System;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class RobotMovement : MonoBehaviour
{
    [Header("Gameplay Related")]
    [Tooltip("Degrees/s")] [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float hoverHeightTarget;
    [SerializeField] private float hoverChangeSpeed;
    [SerializeField] LayerMask groundLayerMask;
    
    [Header("Leg Animation Related")]
    public float footSpacing;
    public float stepDistance;
    public float lerpSpeed;
    public float stepHeight;
    public float footHeightOffset;
    
    public bool Moving { get => moving; }
    public Vector3 InputDirection { get => inputDirection; }
    
    private bool moving;
    private float hoverHeight;
    private Vector3 inputDirection;
    private GameObject mainCamera;
    private List<IK_Foot_Solver> legs;

    private void Start()
    {
        legs = GetComponentsInChildren<IK_Foot_Solver>().ToList();
        
        foreach (IK_Foot_Solver leg in legs)
        {
            leg.footSpacing = footSpacing;
            leg.stepDistance = stepDistance;
            leg.lerpSpeed = lerpSpeed;
            leg.stepHeight = stepHeight;
            leg.footHeightOffset = footHeightOffset;
        }
        
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        hoverHeight = transform.position.y;
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        
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

    private void OnValidate()
    {
        legs = GetComponentsInChildren<IK_Foot_Solver>().ToList();
        
        foreach (IK_Foot_Solver leg in legs)
        {
            leg.footSpacing = footSpacing;
            leg.stepDistance = stepDistance;
            leg.lerpSpeed = lerpSpeed;
            leg.stepHeight = stepHeight;
            leg.footHeightOffset = footHeightOffset;
        }
    }
}
