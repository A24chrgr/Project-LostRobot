using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

namespace Grupp14
{
    public class PlayerHandler : MonoBehaviour
    {
        private GameObject player1, player2;
        [SerializeField] private PlayersMidPoint mPoint;

        void Awake()
        {
            GetPlayerObjects();
            SetControllers();
            if(player1 == null) SetmPoint(player2);
            if(player2 == null) SetmPoint(player1);
            SetmPoint(player1, player2);
        }
        void Update()
        {
        }
        void GetPlayerObjects()
        {
            player1 = GameObject.FindGameObjectWithTag("Ralos");
            player2 = GameObject.FindGameObjectWithTag("Mango");
            if(player1 == null && player2 == null) throw new Exception("No player objects found with the tag 'Ralos' or 'Mango'");
        }

        void SetControllers()
        {
            if (Gamepad.all.ToArray().Length == 1)
            {
                Debug.LogWarning("1 controller connected, defaulting player 2 to keyboard & mouse");
                player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
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
                player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
                player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
            }

        }
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
