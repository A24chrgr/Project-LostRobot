using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Grupp14
{
    public enum TileType
    {
        Normal,
        Blocked,
        Goal
    }
    
    public class GridTile : MonoBehaviour
    {
        public Vector2Int gridPos;
        public TileType tileType;
        public bool isStartTile;
        public float size;
        private bool drawGizmos = true;
        public bool DrawGizmos {set => drawGizmos = value;}

        private Color GetGizmoColor()
        {
            switch (tileType)
            {
                case TileType.Normal:
                    return Color.green;
                case TileType.Blocked:
                    return Color.red;
                case TileType.Goal:
                    return Color.yellow;
            }
            return Color.white;
        }
        
        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = GetGizmoColor();
                Gizmos.DrawCube(
                    new Vector3(transform.position.x, transform.position.y, transform.position.z), //Position
                    new Vector3(size * 0.9f, 0.1f, size * 0.9f) // Size
                );

                if (isStartTile)
                {
                    Gizmos.color = Color.yellow;
                    var grid = GetComponentInParent<CustomGrid>();
                    Gizmos.DrawWireCube(transform.position + Vector3.up * grid.size/2, new Vector3(grid.size, grid.size, grid.size));
                }
            }
        }

        private void OnValidate()
        {
            #if UNITY_EDITOR
            SceneView.RepaintAll();
            #endif
        }

    }
}
