using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FactMachines
{
    public class FactContext : IFactContext
    {
        public FactContext(bool isOneShoot)
        {
            this.isOneShoot = isOneShoot;
        }
        public bool isOneShoot;
        public bool IsOneShoot { get; }
    }
}