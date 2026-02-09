using System;
using UnityEngine;

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
        public GameObject pushBlockPrefab;
        public float size;
        private bool drawGizmos = true;
        public bool DrawGizmos {set => drawGizmos = value;}

        public CustomGrid grid;

        private void Start()
        {
            grid = gameObject.GetComponentInParent<CustomGrid>();
            
            if (isStartTile)
            {
                var block = Instantiate(pushBlockPrefab, transform.parent);
                block.transform.position += new Vector3(0, size * 0.5f, 0) + transform.localPosition;
                block.GetComponent<PushBlock>().currentTile = this;
            }
        }

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
            }
        }
    }
}
