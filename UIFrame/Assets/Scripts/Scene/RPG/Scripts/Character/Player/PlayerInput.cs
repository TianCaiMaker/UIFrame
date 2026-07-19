using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class PlayerInput : MonoBehaviour
    {
        public InputBlackboard inputBlackBoard;
        void Awake()
        {
            Inputs.InputManager.Instance.OnMoveInputChanged += OnMoveInputChanged;
        }

        private void OnMoveInputChanged(Vector2 input)
        {
            inputBlackBoard.MoveInput = input;
        }
    }
}
