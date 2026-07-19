using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
namespace RPG
{
    public class Loco : CharacterState<StateEnum>
    {
        public InputBlackboard inputBlackboard;
        public InputVelocity inputVelocity;
        public float moveSpeed = 5f;
        [Tooltip("转向灵敏度：值越大转向越快")]
        public float RotateSharpness = 10f;
        private CharacterController controller;
        private void Awake()
        {
            controller = GetComponentInParent<CharacterController>();
        }
        public override void OnLogic()
        {
            Vector3 rawInput = new Vector3(inputBlackboard.MoveInput.x, 0, inputBlackboard.MoveInput.y);
            Vector3 input = rawInput.normalized * moveSpeed;
            if (rawInput.sqrMagnitude > 0.0001f)
            {
                Turn(rawInput.normalized);
            }
            inputVelocity.SetVelocity(input);
        }

        void Turn(Vector3 moveDirection)
        {
            if (moveDirection.sqrMagnitude <= 0f) return;
            Quaternion target = Quaternion.LookRotation(moveDirection, Vector3.up);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, target, Mathf.Clamp01(RotateSharpness * Time.deltaTime));
        }
    }
}

