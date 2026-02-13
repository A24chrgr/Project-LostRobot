using UnityEngine;

namespace Grupp14
{
    public class ChangeAnimationBool : MonoBehaviour
    {
        public Animator animator;
        public string boolName = "myBool";
        public void SetBoolInAnimator(bool value)
        {
            animator.SetBool(boolName, value);
        }
    }
}
