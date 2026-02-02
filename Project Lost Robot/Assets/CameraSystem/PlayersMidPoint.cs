using UnityEngine;

public class PlayersMidPoint : MonoBehaviour
{
    [SerializeField] private Transform player1Transform; //P1
    [SerializeField] private Transform player2Transform; //P2

    private Vector3 vectorFromP1ToP2;
    private Vector3 midPoint;

    void Update()
    {
        vectorFromP1ToP2 = player2Transform.position - player1Transform.position;
        midPoint = player1Transform.position + vectorFromP1ToP2 / 2;
        
        transform.position = midPoint;
    }
}
