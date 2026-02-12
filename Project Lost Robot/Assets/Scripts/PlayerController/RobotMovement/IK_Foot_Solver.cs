using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IK_Foot_Solver : MonoBehaviour
{
    //Serialized Variables
    [HideInInspector] public float footSpacing;
    [HideInInspector] public float stepDistance;
    [HideInInspector] public float lerpSpeed;
    [HideInInspector] public float stepHeight;
    [HideInInspector] public float footHeightOffset;
    
    [SerializeField] private Transform hipJoint;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private IK_Foot_Solver otherLeg;
    [SerializeField] private Leg thisLeg;
    [SerializeField] private RobotMovement movementScript;
    
    //Other Variables
    private Vector3 newPosition;
    private Vector3 oldPosition;
    public bool isGrounded;
    private float lerp;
    private int footSpacingSign;
    
    private enum Leg
    {
        Left,
        Right
    }
    
    private void Start()
    {
        switch (thisLeg)
        {
            case Leg.Left:
                transform.position -= Vector3.forward * stepDistance/4;
                footSpacingSign = -1;
                break;
            case Leg.Right:
                transform.position += Vector3.forward * stepDistance/4;
                footSpacingSign = 1;
                break;
        }
        newPosition = transform.position;
    }

    void Update()
    {
        if (movementScript.Moving) //Robot is moving
        {
            Vector3 hipForward = hipJoint.rotation * Vector3.forward;
            Vector3 hipRight   = hipJoint.rotation * Vector3.right;

            Vector3 rayOrigin = hipJoint.position + (hipForward * stepDistance / 2) + (hipRight * (footSpacing * footSpacingSign));
            Ray ray = new Ray(rayOrigin, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
            {
                info.point += new Vector3(0f,footHeightOffset, 0f);
                if (Vector3.Distance(newPosition, info.point) > stepDistance)
                {
                    if (otherLeg.isGrounded && isGrounded)
                    {
                        isGrounded = false;
                    
                        //Start Lerp
                        lerp = 0f;
                        oldPosition = newPosition;
                        newPosition = info.point;
                    }
                }

                if (!isGrounded)
                {
                    newPosition = info.point;
                }
            }

            if (lerp < 1f)
            {
                Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
                footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

                transform.position = footPosition;
                lerp += Time.deltaTime * lerpSpeed;

                if (lerp >= 1f)
                {
                    isGrounded = true;
                }
            }
            else
            {
                transform.position = newPosition;
            }
        }
        else //Robot is standing still
        {
            Vector3 hipForward = hipJoint.rotation * Vector3.forward;
            Vector3 hipRight   = hipJoint.rotation * Vector3.right;

            Vector3 rayOrigin = hipJoint.position + (hipRight * (footSpacing * footSpacingSign));
            Ray ray = new Ray(rayOrigin, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
            {
                info.point += new Vector3(0f,footHeightOffset, 0f);
                if (Vector3.Distance(newPosition, info.point) > 0.1f)
                {
                    if (otherLeg.isGrounded && isGrounded)
                    {
                        isGrounded = false;
                    
                        //Start Lerp
                        lerp = 0f;
                        oldPosition = newPosition;
                        newPosition = info.point;
                    }
                }
                
                if (!isGrounded)
                {
                    newPosition = info.point;
                }
            }
            
            if (lerp < 1f)
            {
                Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
                footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

                transform.position = footPosition;
                lerp += Time.deltaTime * lerpSpeed;

                if (lerp >= 1f)
                {
                    isGrounded = true;
                }
            }
            else
            {
                transform.position = newPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.05f);
    }
}
