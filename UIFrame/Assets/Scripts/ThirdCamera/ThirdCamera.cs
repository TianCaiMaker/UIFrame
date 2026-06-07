using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
namespace ThirdCameras
{
    [ExecuteAlways]
    public class ThirdCamera : MonoBehaviour
    {

        [Serializable]
        public struct Rig
        {
            public float height;
            public float radius;
            public Transform target;
        }
        public enum UpdateMode
        {
            Update,
            LateUpdate,
            FixedUpdate
        }
        public Rig[] rigs = new Rig[3]
        {
            new Rig(){height = 0.2f, radius = 1.5f, target = null},
            new Rig(){height = 2.0f, radius = 3.0f, target = null},
            new Rig(){height = 3.0f, radius = 1.5f, target = null}
        };
        public Transform cameraPoint;
        public UpdateMode updateMode = UpdateMode.LateUpdate;
        //yaw是绕y轴旋转的角度，从-180到180
        public float yaw = 0f;
        //pitchValue是俯仰量从-1到1，不是角度，-1表示最下方，1表示最上方，0表示水平
        public float pitchValue = 0f;
        /*算法采用贝塞尔曲线，rigs[1]为起点，rigs[0]为终点，所在平面向外的正方形点为控制点
                            *（rigs[1]）----*
            ----------------|
            |               |
            *（rigs[0]）    *控制点
        */
        public void UpdatePosition()
        {
            if (cameraPoint == null)
            {
                Debug.Log("Camera Point is not assigned.");
                return;
            }
            if (rigs == null || rigs.Length < 2)
            {
                Debug.Log("At least two rigs are required.");
                return;
            }

        }
        private Vector3 GetCameraPositon()
        {
            Vector3 startPoint = new Vector3(0f, rigs[1].height, -rigs[1].radius) + transform.position;
            Vector3 endPoint;
            Vector3 controlPoint;
            Vector3 positon;
            if (pitchValue < 0f)
            {
                endPoint = new Vector3(0f, rigs[0].height, -rigs[0].radius) + transform.position;
                if (pitchValue < -1f)
                {
                    pitchValue = -1f;
                }
                controlPoint = new Vector3(0f, rigs[0].height, -rigs[1].radius) + transform.position;
                positon = Tools.BezeirCurve.Bazeir3(startPoint, controlPoint, endPoint, -pitchValue);
            }
            else if (pitchValue > 0f)
            {
                endPoint = new Vector3(0f, rigs[2].height, -rigs[2].radius) + transform.position;
                if (pitchValue > 1f)
                {
                    pitchValue = 1f;
                }
                controlPoint = new Vector3(0f, rigs[2].height, -rigs[1].radius) + transform.position;
                positon = Tools.BezeirCurve.Bazeir3(startPoint, controlPoint, endPoint, pitchValue);
            }
            else
            {
                positon = startPoint;
            }

            // apply yaw rotation (degrees) around transform.up
            yaw = NormalizeAngle(yaw);
            Vector3 offset = positon - transform.position;
            offset = Quaternion.AngleAxis(-yaw, transform.up) * offset;
            positon = transform.position + offset;
            return positon;
        }
        private void ChangeLookAt()
        {
            if(rigs[0].target == null || rigs[1].target == null || rigs[2].target == null || cameraPoint == null)
            {
                return;
            }
            {
                Vector3 lookAtPoint = BezeirCurve.Bazeir3(rigs[0].target.position, rigs[1].target.position, rigs[2].target.position, (pitchValue + 1f) / 2f);

                // make cameraPoint look at lookAtPoint, but ensure local Euler z (roll) is zero
                Vector3 forward = lookAtPoint - cameraPoint.position;
                if (forward.sqrMagnitude < 1e-6f)
                    return;

                Quaternion worldRot = Quaternion.LookRotation(forward.normalized, transform.up);

                if (cameraPoint.parent != null)
                {
                    Quaternion localRot = Quaternion.Inverse(cameraPoint.parent.rotation) * worldRot;
                    Vector3 localEuler = localRot.eulerAngles;
                    localEuler.z = 0f;
                    cameraPoint.localRotation = Quaternion.Euler(localEuler);
                }
                else
                {
                    Vector3 worldEuler = worldRot.eulerAngles;
                    worldEuler.z = 0f;
                    cameraPoint.rotation = Quaternion.Euler(worldEuler);
                }
            }
            
        }

        void Update()
        {
            if (updateMode != UpdateMode.Update)
            {
                return;
            }
            cameraPoint.position = GetCameraPositon();
            ChangeLookAt();
        }
        void LateUpdate()
        {
            if (updateMode != UpdateMode.LateUpdate)
            {
                return;
            }
            cameraPoint.position = GetCameraPositon();
            ChangeLookAt();
        }
        void FixedUpdate()
        {
            if (updateMode != UpdateMode.FixedUpdate)
            {
                return;
            }
            cameraPoint.position = GetCameraPositon();
            ChangeLookAt();
        }

        // 将任意角度规范化到 -180..180
        private static float NormalizeAngle(float angle)
        {
            return Mathf.Repeat(angle + 180f, 360f) - 180f;
        }

        #region Gizmos
        private static readonly Color[] RigColors =
        {
            new Color(1f, 0.35f, 0.35f, 1f),
            new Color(1f, 0.8f, 0.2f, 1f),
            new Color(0.3f, 0.75f, 1f, 1f)
        };
        private void OnDrawGizmos()
        {
            
            if (rigs == null)
            {
                return;
            }

            for (int index = 0; index < rigs.Length; index++)
            {
                Rig rig = rigs[index];
                Color rigColor = RigColors[index % RigColors.Length];
                Vector3 center = transform.TransformPoint(new Vector3(0f, rig.height, 0f));

                DrawRigCircle(center, rig.radius, rigColor);

                if (rig.target == null)
                {
                    continue;
                }

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(rig.target.position, 0.1f);
            }
        }

        private void DrawRigCircle(Vector3 center, float radius, Color color)
        {
            if (radius <= 0f)
            {
                return;
            }

            const int segmentCount = 64;
            Vector3 previousPoint = center + transform.right * radius;
            Gizmos.color = color;

            for (int segment = 1; segment <= segmentCount; segment++)
            {
                float angle = Mathf.PI * 2f * segment / segmentCount;
                Vector3 offset = (transform.right * Mathf.Cos(angle) + transform.forward * Mathf.Sin(angle)) * radius;
                Vector3 nextPoint = center + offset;
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
        }

        // cache previous heights to detect which rig was edited in the Inspector
        [NonSerialized]
        private float[] _lastHeights;

        private void OnValidate()
        {
            if (rigs == null || rigs.Length == 0)
                return;

            if (_lastHeights == null || _lastHeights.Length != rigs.Length)
            {
                _lastHeights = new float[rigs.Length];
                for (int i = 0; i < rigs.Length; i++)
                    _lastHeights[i] = rigs[i].height;
                return;
            }

            int changedIndex = -1;
            for (int i = 0; i < rigs.Length; i++)
            {
                if (!Mathf.Approximately(rigs[i].height, _lastHeights[i]))
                {
                    changedIndex = i;
                    break;
                }
            }

            if (changedIndex == -1)
                return;

            // If user increased the changed rig's height, clamp it to not exceed next rig
            Rig changed = rigs[changedIndex];
            if (changedIndex < rigs.Length - 1 && changed.height > rigs[changedIndex + 1].height)
            {
                changed.height = rigs[changedIndex + 1].height;
                rigs[changedIndex] = changed;
            }
            // If user decreased the changed rig's height, clamp it to not go below previous rig
            if (changedIndex > 0 && changed.height < rigs[changedIndex - 1].height)
            {
                changed.height = rigs[changedIndex - 1].height;
                rigs[changedIndex] = changed;
            }

            // update cache
            for (int i = 0; i < rigs.Length; i++)
                _lastHeights[i] = rigs[i].height;
        }
        #endregion
        
    }

}
