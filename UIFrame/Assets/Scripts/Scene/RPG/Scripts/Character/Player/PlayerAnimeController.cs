using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;
namespace RPG
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimeController : MonoBehaviour
    {
        Animator animator;
        public InputBlackboard inputBlackboard;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputBlackboard.OnMoveInputChanged += SetSpeed;
        }
        private void SetSpeed(Vector2 vector2)
        {
            animator.SetFloat("Speed", vector2.sqrMagnitude);
        }
    }
}