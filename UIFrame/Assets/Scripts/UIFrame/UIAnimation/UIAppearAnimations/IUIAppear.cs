using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    public interface IUIAppear
    {
        public float AppearDuration { get; set; }
        public float DisappearDuration { get; set; }
        public void Appear();
        public void Disappear();
    }
}