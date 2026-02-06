using System;
using System.Linq;
using UnityEngine;

namespace Grupp14
{
    using UnityEngine;

    public class CustomGrid : MonoBehaviour
    {
        public int columns;
        public int rows;
        public float tileSize = 1f;
        public bool showGizmos = true;

        public void GenerateGrid()
        {
            // delete old
            for(int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            // create new
            for(int x = 0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    GameObject tileObj = new GameObject($"Tile_{x}_{y}");
                    tileObj.transform.parent = transform;
                    tileObj.transform.localPosition = new Vector3(x * tileSize, 0, y * tileSize);

                    GridTile tile = tileObj.AddComponent<GridTile>();
                    tile.gridPos = new Vector2Int(x,y);
                    tile.size = tileSize;
                }
            }
        }

        public void GizmoVisibility(bool visible)
        {
            var tiles = GetComponentsInChildren<GridTile>().ToList();
            foreach (var tile in tiles)
            {
                if (visible)
                {
                    tile.DrawGizmos = true;
                }
                else
                {
                    tile.DrawGizmos = false;
                }
            }
        }

        private void OnValidate()
        {
            GizmoVisibility(showGizmos);
        }
    }
}
