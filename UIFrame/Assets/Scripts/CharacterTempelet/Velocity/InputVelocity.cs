using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class InputVelocity : MonoBehaviour, IVelocity
    {
        [SerializeField]
        Vector3 velocity = Vector3.zero;
        public Vector3 Velocity => velocity;
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }
        public void SetVertical(float vertical)
        {
            velocity.y = vertical;
        }
    }
}