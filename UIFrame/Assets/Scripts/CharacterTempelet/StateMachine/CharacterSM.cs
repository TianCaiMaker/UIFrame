using System.Collections.Generic;
using System.Linq;
using StateMachines;
using UnityEngine;
namespace Characters
{
    public class CharacterSM<TStateId> : MonoBehaviour where TStateId : struct
    {
        public enum UpdateMode
        {
            Update,
            FixedUpdate,
            LateUpdate,
        }
        public StateMachine<TStateId> stateMachine = new();
        public CharacterState<TStateId> defaultState;
        public UpdateMode updateMode = UpdateMode.Update;
        public IState<TStateId> ActiveState => stateMachine.ActiveState;
        void Awake()
        {
            CharacterState<TStateId>[] states = GetComponentsInChildren<CharacterState<TStateId>>();
            foreach (var state in states)
            {
                stateMachine.AddState(state);
            }
            if (defaultState == null||!states.Contains(defaultState))
            {
                Debug.LogError("Default state is not set or not found in the states list.");
            }
            stateMachine.Init();
        }
        public void RequestStateChange(TStateId name, bool force = false)
        {
            stateMachine.RequestStateChange(name, force);
        }
        public void RequestDefaultState(bool force = false)
        {
            stateMachine.RequestStateChange(defaultState.Name, force);
        }
        public void Update()
        {
            if (updateMode == UpdateMode.Update)
            {
                stateMachine.OnLogic();
            }
        }
        public void FixedUpdate()
        {
            if (updateMode == UpdateMode.FixedUpdate)
            {
                stateMachine.OnLogic();
            }
        }
        public void LateUpdate()
        {
            if (updateMode == UpdateMode.LateUpdate)
            {
                stateMachine.OnLogic();
            }
        }
    }
}