using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Grupp14
{
    public enum blockState
    {
        moving,
        stationary
    }
    
    public class PushBlock : MonoBehaviour
    {
        public float size;
        [SerializeField] private float angleDeadZone = 10f;
        public CustomGrid grid;
        public Vector2Int gridPos;
        private GameObject robot;
        private blockState state = blockState.stationary;
        public float sizeRatio = 3.5f / 1.3182f;

        private Vector3 oldPosition;
        private Vector3 targetPosition;
        private float lerp;
        [SerializeField] private float lerpTime;

        private void Start()
        {
            robot = GameObject.FindWithTag("Ralos");
            transform.localScale = new Vector3(grid.size / sizeRatio, grid.size / sizeRatio, grid.size / sizeRatio);
            transform.position = grid.GetTile(gridPos).transform.position + Vector3.up * grid.size / sizeRatio;
        }

        private void Update()
        {
            switch (state)
            {
                case blockState.stationary:
                    break;
                case blockState.moving:
                    lerp += Time.deltaTime;
                    transform.position = Vector3.Lerp(oldPosition, targetPosition, lerp/lerpTime);
                    if (lerp / lerpTime >= 1f)
                    {
                        state = blockState.stationary;
                        lerp = 0f;
                    }
                    break;
            }
        }

        private void TryMove(Vector2Int dir)
        {
            var newTile = grid.CheckTile(gridPos, dir);
            if (newTile != null)
            {
                MoveTile(newTile);
            }
        }
        
        private void MoveTile(GridTile newTile)
        {
            //Tile Related
            var currentTile = grid.GetTile(gridPos);
            currentTile.tileType = TileType.Normal;
            currentTile = newTile;
            currentTile.tileType = TileType.Blocked;
            gridPos = currentTile.gridPos;
            
            //Movement Related
            state = blockState.moving;
            oldPosition = transform.position;
            targetPosition = currentTile.transform.position + new Vector3(0f, grid.size / sizeRatio, 0f);
        }
        
        public void Push()
        {
            if (state != blockState.stationary)
            {
                return;
            }
            
            //1. Find out the direction to push by comparing the angle between the block and robot.
            var local = transform.InverseTransformPoint(robot.transform.position);
            var angleResult = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
            
            Debug.Log("Angle: " + angleResult);
            
            //2. Check the direction if there exists a grid tile there which it can move to and then either move/dont move the block.
            if (angleResult > (-45 + angleDeadZone) && angleResult < (45 - angleDeadZone))
            {
                TryMove(Vector2Int.down);
            }
            else if (angleResult > (45 + angleDeadZone) && angleResult < (135 - angleDeadZone))
            {
                TryMove(Vector2Int.left);
            }  else if (angleResult < (-45 - angleDeadZone) && angleResult > (-135 + angleDeadZone))
            {
                TryMove(Vector2Int.right);
            } else if (angleResult > (135 + angleDeadZone) || angleResult < (-135 - angleDeadZone))
            {
                TryMove(Vector2Int.up);
            }

        }
    }
}
