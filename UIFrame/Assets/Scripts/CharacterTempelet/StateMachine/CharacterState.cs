using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;
namespace Characters
{
    public abstract class CharacterState<TStateId> : MonoBehaviour,
        IState<TStateId> where TStateId : struct
    {
        [SerializeField]
        private StateContext<TStateId> inputContext;
        public StateContext<TStateId> stateContext => inputContext;
        public virtual bool NeedsExitTime => false;
        public TStateId Name => inputContext.StateName;
        public IStateMachine<TStateId> Owner { get; set; }

        public virtual void Init() { }
        public virtual void OnEnter() { }
        public virtual void OnLogic() { }
        public virtual void OnExit() { }
        public virtual void OnExitRequest() { }
    }
}