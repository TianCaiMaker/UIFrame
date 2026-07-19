using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class RaycastBox : MonoBehaviour
    {
        public Vector3 centerOffset;
        public Vector3 size = Vector3.one;
        public LayerMask layerMask = ~0;
        public List<Collider> ignoreList = new();
        public bool overlapTrigger = false;
        public Collider[] GetOverlaps()
        {
            Vector3 origin = transform.TransformPoint(centerOffset);
            Collider[] overlaps;
            if (overlapTrigger)
                overlaps = Physics.OverlapBox(origin, size * 0.5f, transform.rotation, layerMask);
            else
                overlaps = Physics.OverlapBox(origin, size * 0.5f, transform.rotation, layerMask, QueryTriggerInteraction.Ignore);
            List<Collider> validColliders = new();

            for (int i = 0; i < overlaps.Length; i++)
            {
                Collider targetCollider = overlaps[i];
                if (targetCollider == null || ignoreList.Contains(targetCollider))
                {
                    continue;
                }

                validColliders.Add(targetCollider);
            }

            return validColliders.ToArray();
        }

        public bool HasHit()
        {
            return GetOverlaps().Length > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(centerOffset), transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = oldMatrix;
        }
    }
}

