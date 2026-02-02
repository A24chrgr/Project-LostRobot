using System;
using UnityEngine;

public class RobotDebugLines : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private RobotMovement robotMovement;
    
    [Header("Transforms")]
    [SerializeField] private Transform head;

    [Header("Targets")]
    [SerializeField] private Transform headTarget;
    private Vector3 headTargetDirection;
    private Vector3 targetDir;
    
    [Header("Debug Lines")]
    [SerializeField] private bool drawDebugLines;
    [SerializeField] private bool drawDebugSpheres;
    [SerializeField] private bool drawTargetLines;
    [SerializeField] private float debugLineLength = 1f;
    [SerializeField] private float debugSphereSize = 0.1f;

    private void Update()
    {
        CalculateDirections();
    }

    private void CalculateDirections()
    {
        headTargetDirection = (headTarget.position - head.position).normalized;
        targetDir = new Vector3(robotMovement.InputDirection.x, 0f, robotMovement.InputDirection.y);
    }

    private void OnDrawGizmos()
    {
        if (drawDebugLines)
        {
            //Current Direction
            Gizmos.color = Color.green;
            Gizmos.DrawLine(head.position, head.position + head.forward * debugLineLength);
            if(drawDebugSpheres)
                Gizmos.DrawSphere(head.position + head.forward * debugLineLength, debugSphereSize);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * debugLineLength);
            if(drawDebugSpheres)
                Gizmos.DrawSphere(transform.position + transform.forward * debugLineLength, debugSphereSize);
        
            //Target Direction
            if (drawTargetLines)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(head.position, head.position + headTargetDirection * debugLineLength);
                if(drawDebugSpheres)
                    Gizmos.DrawSphere(head.position + headTargetDirection * debugLineLength, debugSphereSize);
                Gizmos.DrawLine(transform.position, transform.position + targetDir * debugLineLength);
                if(drawDebugSpheres)
                    Gizmos.DrawSphere(transform.position + targetDir * debugLineLength, debugSphereSize);
                
            }
        }
    }
}
