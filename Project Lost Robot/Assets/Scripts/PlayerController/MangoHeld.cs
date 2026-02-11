using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grupp14
{
    public class MangoHeld : MonoBehaviour
    {
        private PlayerInput playerInput;
        private InputAction jumpAction, moveAction;
        public bool isHeld;
        [NonSerialized] public PlayerPickUp ppU;


        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            jumpAction = playerInput.actions.FindAction("Jump");
            moveAction = playerInput.actions.FindAction("Move");
        }
        void Update()
        {
            if (!isHeld) return;
            if (jumpAction.WasPressedThisFrame() || moveAction.WasPressedThisFrame())
            {
                ppU.DropMango();
            }
        }
    }
}
