using UnityEngine;

namespace Grupp14
{
    public class MeshRendererManager : MonoBehaviour
    {
        public void ToggleMeshRenderers()
        {
            foreach (MeshRenderer renderer in  GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = !renderer.enabled;
            }
        }
    }
}