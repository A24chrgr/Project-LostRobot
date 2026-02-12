using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grupp14
{
    using UnityEngine;

    public class CustomGrid : MonoBehaviour
    {
        public int columns;
        public int rows;
        public bool showGizmos = true;
        public float size;
        
        [SerializeField] private PushBlock blockPrefab;
        
        private readonly Dictionary<Vector2Int, GridTile> tiles = new Dictionary<Vector2Int, GridTile>();
        
        private void Awake()
        {
            RebuildDictionary();
        }

        private void Start()
        {
            //Instantiate all blocks
            foreach (var tile in tiles.Values)
            {
                if (tile.isStartTile)
                {
                    var block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
                    block.grid = this;
                    block.gridPos = tile.gridPos;
                }
            }
        }

        private void RebuildDictionary()
        {
            tiles.Clear();

            var existingTiles = GetComponentsInChildren<GridTile>();

            foreach (var tile in existingTiles)
            {
                tiles[tile.gridPos] = tile;
                tile.size = size;
                tile.transform.localPosition = new Vector3(tile.gridPos.x * size, 0, tile.gridPos.y * size);
            }
        }
        
        public void GenerateGrid()
        {
            // delete old
            tiles.Clear();
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
                    tileObj.transform.localPosition = new Vector3(x * size, 0, y * size);

                    GridTile tile = tileObj.AddComponent<GridTile>();
                    tile.gridPos = new Vector2Int(x,y);
                    tile.size = size;
                    
                   //Add tile to the Dictionary.
                   tiles.Add(tile.gridPos, tile);
                }
            }
        }

        public GridTile CheckTile(Vector2Int currentPos, Vector2Int checkDir)
        {
            var newTile = GetTile(currentPos + checkDir);
            if (newTile != null && newTile.tileType != TileType.Blocked)
            {
                return newTile;
            }

            return null;
        }
        
        public GridTile GetTile(Vector2Int gridPos)
        {
           if(tiles.TryGetValue(gridPos, out GridTile tile)) 
               return tile;
           
           return null;
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
            RebuildDictionary();
            GizmoVisibility(showGizmos);
            foreach (GridTile tile in tiles.Values)
            {
                tile.size = size;
                tile.transform.localPosition = new Vector3(tile.gridPos.x * size, 0, tile.gridPos.y * size);
            }
        }
    }
}
