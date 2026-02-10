using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Grupp14
{
    public class PlayerHandler : MonoBehaviour
    {
        private GameObject player1, player2;
        [SerializeField] private PlayersMidPoint mPoint;
        [SerializeField] private bool flipControllers;
        private int p1Value = 0, p2Value = 1;

        void Awake()
        {
            GetPlayerObjects();
            SetControllers();
            SetmPoint(player1, player2);
        }
        void Update()
        {
/*             if (flipControllers)
            {
                FlipControllers();
                flipControllers = false;
            } */
        }
        void GetPlayerObjects()
        {
            player1 = GameObject.FindGameObjectWithTag("Ralos");
            player2 = GameObject.FindGameObjectWithTag("Mango");
        }

        void SetControllers()
        {
            if (Gamepad.all.ToArray().Length == 1)
            {
                Debug.LogWarning("1 controller connected, defaulting player 2 to keyboard & mouse");
                player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[p1Value % 2]);
                player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            }
            else if (Gamepad.all.ToArray().Length == 0)
            {
                Debug.LogWarning("0 controller connected, defaulting both players to keyboard & mouse");
                player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
                player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            }
            else
            {
                player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[p1Value % 2]);
                player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[p2Value % 2]);
                // p1Value++;
                // p2Value++;
            }

        }
/*         void FlipControllers()
        {
            SetControllers();
        } */
        void SetmPoint(GameObject slot1, GameObject slot2)
        {
            mPoint.player1Transform = slot1.transform;
            mPoint.player2Transform = slot2.transform;
        }
        void SetmPoint(GameObject dualSlot)
        {
            mPoint.player1Transform = dualSlot.transform;
            mPoint.player2Transform = dualSlot.transform;
        }
    }
}
