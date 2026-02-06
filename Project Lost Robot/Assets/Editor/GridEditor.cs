using UnityEditor;
using UnityEngine;
using Grupp14;

[CustomEditor(typeof(CustomGrid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CustomGrid grid = (CustomGrid)target;

        if(GUILayout.Button("Generate Grid"))
        {
            grid.GenerateGrid();
        }
    }
}