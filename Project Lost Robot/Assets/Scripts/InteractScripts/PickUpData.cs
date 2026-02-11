using UnityEngine;

namespace Grupp14
{
    public class PickUpData : MonoBehaviour
    {
        [SerializeField] bool isAllowedAnimal = true, isAllowedRobot = true;

        public bool CheckIfAllowed(string sender)
        {
            if(sender == "Ralos" && !isAllowedRobot) return false;
            else if(sender == "Mango" && !isAllowedAnimal) return false;
            else
            {
                return true;
            }
        }
    }
}
