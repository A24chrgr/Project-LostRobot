using System;
using UnityEngine;

namespace Grupp14
{
    public class PushBlock : MonoBehaviour
    {
        [SerializeField] private float angleDeadZone = 10f;
        public float size;
        public GridTile currentTile;
        private GameObject robot;

        private void Start()
        {
            robot = GameObject.Find("Robot");
        }

        private void Update()
        {
            transform.position = currentTile.transform.position + new Vector3(0f, size/2, 0f);
        }

        public void Push()
        {
            //1. Find out the direction to push by comparing the angle between the block and robot.
            var local = transform.InverseTransformPoint(robot.transform.position);
            var angleResult = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
            
            Debug.Log("Angle: " + angleResult);
            
            //2. Check the direction if there exists a grid tile there which it can move to and then either move/dont move the block.
            if (angleResult > (-45 + angleDeadZone) && angleResult < (45 - angleDeadZone)) //Up
            {
                var upTile = currentTile.grid.GetTile(currentTile.gridPos + Vector2Int.down);
                if (upTile != null && upTile.tileType != TileType.Blocked)
                {
                    currentTile = upTile;
                }
            }
            else if (angleResult > (45 + angleDeadZone) && angleResult < (135 - angleDeadZone) ) //Right
            {
                var rightTile = currentTile.grid.GetTile(currentTile.gridPos + Vector2Int.left);
                if (rightTile != null && rightTile.tileType != TileType.Blocked)
                {
                    currentTile = rightTile;
                }
            }  else if (angleResult < (-45 - angleDeadZone) && angleResult > (-135 + angleDeadZone)) //Left
            {
                var leftTile = currentTile.grid.GetTile(currentTile.gridPos + Vector2Int.right);
                if (leftTile != null && leftTile.tileType != TileType.Blocked)
                {
                    currentTile = leftTile;
                }
            } else if (angleResult > (135 + angleDeadZone) || angleResult < (-135 - angleDeadZone)) //Down
            {
                var downTile = currentTile.grid.GetTile(currentTile.gridPos + Vector2Int.up);
                if (downTile != null && downTile.tileType != TileType.Blocked)
                {
                    currentTile = downTile;
                }
            }

        }
    }
}
