using System;
using System.Collections;
using System.Collections.Generic;
using FactMachines;
using General.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Inputs
{
    public enum InputKeys
    {
        Move,
    }


    public class InputManager : SingletonMono<InputManager>, GameInput.IKeyboardActions
    {
        public FactMachine<InputKeys> factMachine { get; private set; } = new();
        GameInput gameInput;
        private Vector2 moveInput;
        public Vector2 MoveInput
        {
            get
            {
                return moveInput;
            }
            private set
            {
                if (moveInput != value)
                {
                    moveInput = value;
                    OnMoveInputChanged?.Invoke(moveInput);
                }
            }
        }
        public event Action<Vector2> OnMoveInputChanged;
        public override void Awake()
        {
            base.Awake();
            gameInput = new GameInput();
            gameInput.Keyboard.Move.performed += OnMove;
            gameInput.Keyboard.Move.canceled += OnMove;
            gameInput.Enable();
        }
        private void RegisterKey(InputKeys key,bool isOneShoot)
        {
            factMachine.RegisterFact(InputKeys.Move, new FactContext(isOneShoot));
        }
        void OnDestroy()
        {
            gameInput.Keyboard.Move.performed -= OnMove;
            gameInput.Keyboard.Move.canceled -= OnMove;
            gameInput.Disable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
    }

}