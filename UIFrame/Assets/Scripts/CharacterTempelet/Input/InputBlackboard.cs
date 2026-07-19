using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class InputBlackboard : MonoBehaviour
    {
        private Vector2 moveInput;
        public event System.Action<Vector2> OnMoveInputChanged;
        public Vector2 MoveInput
        {
            get => moveInput;
            set
            {
                if (moveInput != value)
                {
                    moveInput = value;
                    OnMoveInputChanged?.Invoke(moveInput);
                }
            }
        }
    }
}

