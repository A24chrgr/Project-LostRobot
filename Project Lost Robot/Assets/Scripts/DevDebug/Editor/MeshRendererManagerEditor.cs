using UnityEditor;
using UnityEngine;

namespace Grupp14.Editor
{
    [CustomEditor(typeof(MeshRendererManager))]
    public class MeshRendererManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Toggle Mesh Renderers"))
            {
                ((MeshRendererManager)target).ToggleMeshRenderers();
            }
        }
    }
}