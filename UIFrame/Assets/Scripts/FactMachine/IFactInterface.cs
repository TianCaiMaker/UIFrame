using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FactMachines
{
    public interface IFactContext
    {
        public bool IsOneShoot { get; }
    }
    public interface IFactListener<TFact> where TFact : struct
    {
        //不要在get方法里更改集合，最好永远不要更改集合
        public HashSet<TFact> Facts { get; }
        public void OnFactTrigger(object source, TFact fact);
    }
}