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
        public TFact Fact { get; }
        public void OnFactTrigger(object source, TFact fact);
    }
}