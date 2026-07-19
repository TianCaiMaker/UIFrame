using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{

    public class GroundRayCast : MonoBehaviour
    {
        public RaycastBox box;
        public bool isGrounded { get; private set; }

        [SerializeField] int ungroundedFrameCount = 4;
        float rayLength = 0.6f;

        int currentUngroundedFrameCount;
        bool hasInitializedGroundedState;
        // 获取Ground层的LayerMask
        int groundLayer;
        Vector3 groundNormal;
        public Vector3 GroundNormal => groundNormal;
        private void Awake()
        {
            groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void FixedUpdate()
        {
            UpdateIsGrounded();
            groundNormal = GetGroundNormalOrUp();
        }
        private void UpdateIsGrounded()
        {
            if (!box)
            {
                isGrounded = false;
                currentUngroundedFrameCount = 0;
                hasInitializedGroundedState = true;
            }
            else
            {
                bool hasHit = box.HasHit();
                int targetUngroundedFrameCount = Mathf.Max(1, ungroundedFrameCount);

                if (!hasInitializedGroundedState)
                {
                    hasInitializedGroundedState = true;
                    isGrounded = hasHit;
                    currentUngroundedFrameCount = hasHit ? 0 : 1;
                }
                else if (hasHit)
                {
                    currentUngroundedFrameCount = 0;
                    isGrounded = true;
                }
                else
                {
                    currentUngroundedFrameCount++;
                    if (currentUngroundedFrameCount >= targetUngroundedFrameCount)
                    {
                        isGrounded = false;
                    }
                }
            }
        }//UpdateIsGrounded

        /// <summary>
        /// 检测地面并返回法线，如果未检测到则返回Vector3.up
        /// </summary>
        /// <returns>地面法线或Vector3.up</returns>
        Vector3 GetGroundNormalOrUp()
        {
            if (box == null)
                return Vector3.up;

            if (!isGrounded)
                return Vector3.up;

            // 获取box的中心点
            Vector3 origin = box.transform.TransformPoint(box.centerOffset);
            int groundMask = 1 << groundLayer;
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.down, out hit, rayLength, groundMask, QueryTriggerInteraction.Ignore))
            {
                //Debug.Log(hit.normal);
                return hit.normal;
            }
            return Vector3.up;
        }

        public Vector3 GetGroundVelocity(Vector3 velocity)
        {
            // 投影到地面
            Vector3 projected = Vector3.ProjectOnPlane(velocity, GroundNormal);

            // 保持原速度大小
            if (projected.sqrMagnitude > 0.0001f)
                projected = projected.normalized * velocity.magnitude;
            return projected;
        }

    }//Class GroundRayCast
}