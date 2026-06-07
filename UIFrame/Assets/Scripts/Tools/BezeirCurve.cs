using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tools
{
    public static class BezeirCurve
    {
        public static Vector3 Bazeir3(Vector3 start, Vector3 control, Vector3 end, float t)
        {
            float u = 1 - t;
            return u * u * start + 2 * u * t * control + t * t * end;
        }
    }
}