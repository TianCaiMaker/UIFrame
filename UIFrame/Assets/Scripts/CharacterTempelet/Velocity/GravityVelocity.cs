using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class GravityVelocity : MonoBehaviour, IVelocity
    {
        public GroundRayCast ground;
        public float gravityScale = 1f;
        private Vector3 velocity;
        public Vector3 Velocity => velocity;
        private bool wasGrounded;
        private void FixedUpdate()
        {
            //Debug.Log(Velocity);
            if (!ground)
            {
                velocity = Vector3.zero;
                return;
            }
            if (wasGrounded && !ground.isGrounded)
            {
                velocity = Vector3.zero;
            }
            if (ground.isGrounded)
            {
                velocity = -Vector3.up;
            }
            else
            {
                velocity += -9.8f * gravityScale * Time.fixedDeltaTime * Vector3.up;
            }
            wasGrounded = ground.isGrounded;
            //Debug.Log($"GravityVelocity: {velocity}");
        }
        public void ResetVelocity()
        {
            velocity = Vector3.zero;
        }
    }
}