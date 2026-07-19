using System.Collections;
using System.Collections.Generic;
using FactMachines;
using UnityEngine;
namespace Characters
{
    public class Intention<TStateId> : MonoBehaviour, IFactListener<TStateId> where TStateId : struct
    {
        private FactMachine<TStateId> factMachine = new();
        public CharacterSM<TStateId> characterSM;
        private HashSet<TStateId> facts = new HashSet<TStateId>();
        public HashSet<TStateId> Facts => facts;

        void Awake()
        {
            CharacterState<TStateId>[] states = characterSM.GetComponentsInChildren<CharacterState<TStateId>>();
            foreach (var state in states)
            {
                factMachine.RegisterFact(state.Name, state.stateContext);
                if (!facts.Add(state.Name))
                {
                    Debug.LogWarning($"Duplicate fact {state.Name} found in Intention.");
                }
            }
            factMachine.RegisterListener(this);
        }
        public void AddFact(TStateId fact)
        {
            factMachine.AddFact(this,fact);
        }

        public void OnFactTrigger(object source, TStateId fact)
        {
            if(factMachine.HasFact(fact))
            {
                JudgeStateChange(fact);
            }
        }
        private void JudgeStateChange(TStateId fact)
        {
            if (factMachine.GetFactContext(fact) is StateContext<TStateId> current)
            {
                TStateId activeState = characterSM.ActiveState.Name;
                if (factMachine.GetFactContext(activeState) is StateContext<TStateId> activeContext)
                {
                    if (current.CanInterruptOther(activeContext))
                    {
                        characterSM.RequestStateChange(fact);
                    }
                }
                else
                {
                    Debug.LogWarning($"Active state {activeState} does not have a corresponding context.");
                }
            }
            else
            {
                Debug.LogWarning($"Fact {fact} does not have a corresponding context.");
            }
        }

    }
}

