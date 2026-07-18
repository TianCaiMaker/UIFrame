using System;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachines
{
    public class StateMachine<TOnwnerId, TStateId, TEvent> : IState<TOnwnerId>, IStateMachine<TStateId>
    {
        #region StateBundle
        /// <summary>
		/// A bundle of a state together with the outgoing transitions and trigger transitions.
		/// It's useful, as you only need to do one Dictionary lookup for these three items.
		/// => Much better performance
		/// </summary>
		private class StateBundle
        {
            // By default, these fields are all null and only get a value when you need them
            // => Lazy evaluation => Memory efficient, when you only need a subset of features
            public IState<TStateId> state;
            // A list of all transitions that go out of this state. No trigger transitions, just normal transitions.
            public List<TransitionBase<TStateId>> transitions;
            public Dictionary<TEvent, List<TransitionBase<TStateId>>> triggerToTransitions;

            public void AddTransition(TransitionBase<TStateId> t)
            {
                transitions = transitions ?? new List<TransitionBase<TStateId>>();
                transitions.Add(t);
            }

            public void AddTriggerTransition(TEvent trigger, TransitionBase<TStateId> transition)
            {
                triggerToTransitions = triggerToTransitions
                    ?? new Dictionary<TEvent, List<TransitionBase<TStateId>>>();

                List<TransitionBase<TStateId>> transitionsOfTrigger;

                if (!triggerToTransitions.TryGetValue(trigger, out transitionsOfTrigger))
                {
                    transitionsOfTrigger = new List<TransitionBase<TStateId>>();
                    triggerToTransitions.Add(trigger, transitionsOfTrigger);
                }

                transitionsOfTrigger.Add(transition);
            }
        }
        #endregion
        #region StateMachine fields
        public TOnwnerId Name { get; }
        private bool isAutoChange;
        public bool IsAutoChange => isAutoChange;
        private IStateMachine<TOnwnerId> owner;
        public IStateMachine<TOnwnerId> Owner
        {
            get => owner;
            set => owner = value;
        }
        private bool IsRootFsm => Owner == null;
        private IState<TStateId> activeState = null;
        public IState<TStateId> ActiveState
        {
            get
            {
                EnsureIsInitializedFor("Trying to get the active state");
                return activeState;
            }
        }
        public TStateId ActiveStateName => ActiveState.Name;


        // A cached empty list of transitions (For improved readability, less GC)
        private static readonly List<TransitionBase<TStateId>> noTransitions
            = new List<TransitionBase<TStateId>>(0);
        private static readonly Dictionary<TEvent, List<TransitionBase<TStateId>>> noTriggerTransitions
        = new Dictionary<TEvent, List<TransitionBase<TStateId>>>(0);

        private (TStateId state, bool hasState) startState = (default, false);
        private (TStateId state, bool isPending) pendingState = (default, false);

        // Central storage of states
        private Dictionary<TStateId, StateBundle> nameToStateBundle
            = new Dictionary<TStateId, StateBundle>();
        private List<TransitionBase<TStateId>> activeTransitions = noTransitions;
        private Dictionary<TEvent, List<TransitionBase<TStateId>>> activeTriggerTransitions = noTriggerTransitions;
        private List<TransitionBase<TStateId>> transitionsFromAny
            = new List<TransitionBase<TStateId>>();
        private Dictionary<TEvent, List<TransitionBase<TStateId>>> triggerTransitionsFromAny
        = new Dictionary<TEvent, List<TransitionBase<TStateId>>>();

        #endregion
        #region StateMachine methods

        /// <summary>
		/// Initialises a new instance of the StateMachine class
		/// </summary>
		/// <param name="IsAutoChange">Determines if the state machine should automatically change states (true), or wait until the active state is ready for a state change (false)</param>
		public StateMachine(bool IsAutoChange = true)
        {
            this.isAutoChange = IsAutoChange;
        }
        public void StateCanExit()
        {
            if (pendingState.isPending)
            {
                //ChangeState(pendingState.state);
                pendingState = (default, false);
            }

            Owner?.StateCanExit();
        }
        
        public void Init()
        {
        }

        public void OnEnter()
        {
        }

        public void OnLogic()
        {
        }

        public void OnExit()
        {
        }

        public void OnExitRequest()
        {
        }

        private void EnsureIsInitializedFor(string context)
        {
            if (activeState == null)
                Debug.LogError($"State machine is not initialized for {context}. Call Init() first.");
        }

        public void RequestStateChange(TStateId name, bool forceInstantly = false)
        {
        }
    }
    #endregion
#region Overloaded classes
    // Overloaded classes to allow for an easier usage of the StateMachine for common cases.
    // E.g. new StateMachine() instead of new StateMachine<string, string, string>()

    public class StateMachine<TStateId, TEvent> : StateMachine<TStateId, TStateId, TEvent>
    {
        public StateMachine(bool IsAutoChange = true) : base(IsAutoChange)
        {
        }
    }

    public class StateMachine<TStateId> : StateMachine<TStateId, TStateId, string>
    {
        public StateMachine(bool IsAutoChange = true) : base(IsAutoChange)
        {
        }
    }

    public class StateMachine : StateMachine<string, string, string>
    {
        public StateMachine(bool IsAutoChange = true) : base(IsAutoChange)
        {
        }
    }
#endregion
}
