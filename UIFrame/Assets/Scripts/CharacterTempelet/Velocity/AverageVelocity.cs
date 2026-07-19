using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{
    public class AverageVelocity : MonoBehaviour
    {
        [Tooltip("用于计算平均速度的采样帧数（至少2）")]
        public int sampleFrames = 10;

        // 环形队列用于存储最近采样的位置信息
        Queue<Vector3> samples = new Queue<Vector3>();

        void FixedUpdate()
        {
            samples.Enqueue(transform.position);
            while (samples.Count > Mathf.Max(2, sampleFrames))
                samples.Dequeue();
        }

        /// <summary>
        /// 获取最近采样帧的平均速度（位置差除以总时间）
        /// 如果样本不足返回 Vector3.zero
        /// </summary>
        /// <returns>平均速度（世界坐标）</returns>
        public Vector3 GetAverageVelocity()
        {
            if (samples.Count < 2)
                return Vector3.zero;

            Vector3[] arr = samples.ToArray();
            Vector3 first = arr[0];
            Vector3 last = arr[arr.Length - 1];
            float intervals = arr.Length - 1;
            float totalTime = intervals * Time.fixedDeltaTime;
            if (totalTime <= Mathf.Epsilon)
                return Vector3.zero;

            return (last - first) / totalTime;
        }
    }
}