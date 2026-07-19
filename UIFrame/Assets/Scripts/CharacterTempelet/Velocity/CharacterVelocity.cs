using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Characters
{
    interface IVelocity
    {
        public Vector3 Velocity { get; }
    }
    public class CharacterVelocity : MonoBehaviour
    {
        List<IVelocity> velocitys = new();
        CharacterController controller;
        private void Awake()
        {
            velocitys = GetComponentsInChildren<IVelocity>().ToList();
            controller = GetComponentInParent<CharacterController>();
        }
        private void FixedUpdate()
        {
            Vector3 velocity = Vector3.zero;
            foreach (IVelocity v in velocitys)
            {
                velocity += v.Velocity;
            }
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }
}
