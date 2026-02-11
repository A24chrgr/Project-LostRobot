using UnityEngine;
using UnityEngine.UIElements;

namespace Grupp14
{
    public class ClimbData : MonoBehaviour
    {
        public Vector3 normal;
        [SerializeField] bool toggleRay;
        void Update()
        {
            if(toggleRay) Debug.DrawRay(transform.position + GetComponent<BoxCollider>().center, normal.normalized*3, Color.red);
        }
    }
}
