using System.Collections.Generic;
using FactMachines;
using UnityEngine;
namespace Characters
{
    public enum InterruptBySamePriorityRule
    {
        OnlyInWhiteList,
        Never,
        Always,
    }
    public class StateContext<TStateId> : ScriptableObject, IFactContext where TStateId : struct
    {
        [SerializeField]
        private bool isOneShoot;
        [SerializeField]
        private TStateId stateName;
        [SerializeField]
        private int priority;
        [SerializeField]
        private List<TStateId> canInterruptStates;
        [SerializeField]
        private InterruptBySamePriorityRule interruptBySamePriority = InterruptBySamePriorityRule.OnlyInWhiteList;
        public bool IsOneShoot => isOneShoot;
        public TStateId StateName => stateName;
        public int Priority => priority;
        public InterruptBySamePriorityRule InterruptBySamePriority => interruptBySamePriority;
        //只有相同优先级才会考虑打断列表，里面是可以打断目标状态的白名单
        public List<TStateId> CanInterruptStates => canInterruptStates;
        public bool CanInterruptOther(StateContext<TStateId> other)
        {
            if (this.Priority > other.Priority)
            {
                return true;
            }
            else if (this.Priority == other.Priority)
            {
                switch (other.InterruptBySamePriority)
                {
                    case InterruptBySamePriorityRule.Never:
                        return false;
                    case InterruptBySamePriorityRule.Always:
                        return true;
                    case InterruptBySamePriorityRule.OnlyInWhiteList:
                        return this.canInterruptStates.Contains(other.StateName);
                }
            }
            return false;
        }
    }
}