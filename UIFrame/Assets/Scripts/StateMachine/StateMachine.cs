using System.Collections.Generic;
namespace StateMachines
{
    public class StateMachine<TOnwnerId, TStateId, TEvent> :
     IState<TOnwnerId>,
      IStateMachine<TStateId>,
      ITriggerable<TEvent>
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
        private TOnwnerId name;
        public TOnwnerId Name { get => name; set => name = value; }
        private bool needsExitTime;
        public bool NeedsExitTime => needsExitTime;
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
		/// <param name="needsExitTime">(Only for hierarchical states):
		/// 	Determins whether the state machine as a state of a parent state machine is allowed to instantly
		/// 	exit on a transition (false), or if it should wait until the active state is ready for a
		/// 	state change (true).
        /// </param>
		public StateMachine(bool needsExitTime = false)
        {
            this.needsExitTime = needsExitTime;
        }
        /// <summary>
        ///     Try to exit the state machine, if there is a state to switch to, switch to that state
        /// 	尝试退出状态机，如果有要切换的状态，就切换到那个状态
        /// </summary>
        public void StateCanExit()
        {
            if (pendingState.isPending)
            {
                ChangeState(pendingState.state);
                pendingState = (default, false);
            }

            Owner?.StateCanExit();
        }
        /// <summary>
		/// Instantly changes to the target state
		/// </summary>
		/// <param name="name">The name / identifier of the active state</param>
		private void ChangeState(TStateId name)
        {
            activeState?.OnExit();

            StateBundle bundle;

            if (!nameToStateBundle.TryGetValue(name, out bundle) || bundle.state == null)
            {
                throw new Exceptions.StateNotFoundException<TStateId>(name, "Switching states");
            }

            activeTransitions = bundle.transitions ?? noTransitions;
            activeTriggerTransitions = bundle.triggerToTransitions ?? noTriggerTransitions;

            activeState = bundle.state;
            activeState.OnEnter();

            for (int i = 0; i < activeTransitions.Count; i++)
            {
                activeTransitions[i].OnEnter();
            }

            foreach (List<TransitionBase<TStateId>> transitions in activeTriggerTransitions.Values)
            {
                for (int i = 0; i < transitions.Count; i++)
                {
                    transitions[i].OnEnter();
                }
            }
        }
        /// <summary>
		/// Requests a state change, respecting the <c>needsExitTime</c> property of the active state
        /// 请求切换到指定的状态，但是要考虑当前状态的 needsExitTime 属性，如果当前状态需要退出时机，则会等待当前状态调用 fsm.StateCanExit() 来切换状态
		/// </summary>
		/// <param name="name">The name / identifier of the target state</param>
		/// <param name="forceInstantly">Overrides the needsExitTime of the active state if true,
		/// therefore forcing an immediate state change</param>
		public void RequestStateChange(TStateId name, bool forceInstantly = false)
        {
            if (!activeState.NeedsExitTime || forceInstantly)
            {
                ChangeState(name);
            }
            else
            {
                pendingState = (name, true);
                activeState.OnExitRequest();
                /**
				 * If it can exit, the activeState would call
				 * -> state.fsm.StateCanExit() which in turn would call
				 * -> fsm.ChangeState(...)
				 */
            }
        }
        /// <summary>
        /// Checks if a transition can take place, and if this is the case, transition to the
        /// "to" state and return true. Otherwise it returns false.
        /// 检查一个transition的条件，无论from状态是否是当前状态，只要满足条件就会切换到to状态
        /// 返回条件是否满足，切换受到forceInstantly和当前转台的needsExitTime影响.
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        private bool TryTransition(TransitionBase<TStateId> transition)
        {
            if (!transition.ShouldTransition())
                return false;

            RequestStateChange(transition.to, transition.forceInstantly);

            return true;
        }
        /// <summary>
		/// Defines the entry point of the state machine
		/// </summary>
		/// <param name="name">The name / identifier of the start state</param>
		public void SetStartState(TStateId name)
        {
            startState = (name, true);
        }
        /// <summary>
		/// Calls OnEnter if it is the root machine, therefore initialising the state machine
        /// 如果是根状态机，那么调用OnEnter方法来初始化状态机
        /// 所以根状态机一定要先调用这个方法
		/// </summary>
		public void Init()
        {
            if (!IsRootFsm) return;

            OnEnter();
        }
        /// <summary>
		/// Initialises the state machine and must be called before OnLogic is called.
		/// It sets the activeState to the selected startState.
		/// </summary>
		public void OnEnter()
        {
            if (!startState.hasState)
            {
                throw new System.InvalidOperationException(
                    Exceptions.ExceptionFormatter.Format(
                    context: "Running OnEnter of the state machine.",
                    problem: "No start state is selected. "
                            + "The state machine needs at least one state to function properly.",
                    solution: "Make sure that there is at least one state in the state machine "
                            + "before running Init() or OnEnter() by calling fsm.AddState(...)."
                    )
                );
            }
            ChangeState(startState.state);

            for (int i = 0; i < transitionsFromAny.Count; i++)
            {
                transitionsFromAny[i].OnEnter();
            }

            foreach (List<TransitionBase<TStateId>> transitions in triggerTransitionsFromAny.Values)
            {
                for (int i = 0; i < transitions.Count; i++)
                {
                    transitions[i].OnEnter();
                }
            }
        }

        /// <summary>
		/// Runs one logic step. It does at most one transition itself and
		/// calls the active state's logic function (after the state transition, if
		/// one occurred).
        /// 先判断切换再调用当前状态的OnLogic方法，如果切换了状态，那么调用的是新状态的OnLogic方法
        /// 如果状态NeedsExitTime为true，那么在OnLogic中不会切换状态，而是调用子状态的OnExitRequest方法
        /// 等待当前状态调用 fsm.StateCanExit() 来切换状态
		/// </summary>
		public void OnLogic()
        {
            EnsureIsInitializedFor("Running OnLogic");

            // Try the "global" transitions that can transition from any state
            for (int i = 0; i < transitionsFromAny.Count; i++)
            {
                TransitionBase<TStateId> transition = transitionsFromAny[i];

                // Don't transition to the "to" state, if that state is already the active state
                if (EqualityComparer<TStateId>.Default.Equals(transition.to, activeState.Name))
                    continue;

                if (TryTransition(transition))
                    break;
            }

            // Try the "normal" transitions that transition from one specific state to another
            for (int i = 0; i < activeTransitions.Count; i++)
            {
                TransitionBase<TStateId> transition = activeTransitions[i];

                if (TryTransition(transition))
                    break;
            }

            activeState.OnLogic();
        }

        public void OnExit()
        {
            if (activeState != null)
            {
                activeState.OnExit();
                // By setting the activeState to null, the state's onExit method won't be called
                // a second time when the state machine enters again (and changes to the start state)
                activeState = null;
            }
        }
        /// <summary>
        ///   Called when a state need to exit, if the state can exit, it should call fsm.StateCanExit(),
        ///  当需要退出状态机且满足切换条件但是没有切换的时候调用，等待子状态调用 fsm.StateCanExit() 来切换状态
        /// </summary>
        public void OnExitRequest()
        {
            if (activeState.NeedsExitTime)
            {
                activeState.OnExitRequest();
                return;
            }

            Owner?.StateCanExit();
        }
        /// <summary>
		/// Gets the StateBundle belonging to the <c>name</c> state "slot" if it exists.
		/// Otherwise it will create a new StateBundle, that will be added to the Dictionary,
		/// and return the newly created instance.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private StateBundle GetOrCreateStateBundle(TStateId name)
        {
            StateBundle bundle;

            if (!nameToStateBundle.TryGetValue(name, out bundle))
            {
                bundle = new StateBundle();
                nameToStateBundle.Add(name, bundle);
            }

            return bundle;
        }
        /// <summary>
		/// Adds a new node / state to the state machine.
        /// 给状态机添加新的节点，如果是第一个节点，则会自动设置为起始状态
		/// </summary>
		/// <param name="name">The name / identifier of the new state</param>
		/// <param name="state">The new state instance, e.g. <c>State</c>, <c>CoState</c>, <c>StateMachine</c></param>
		public void AddState(TStateId name, IState<TStateId> state)
        {
            state.Owner = this;
            state.Name = name;
            state.Init();

            StateBundle bundle = GetOrCreateStateBundle(name);
            bundle.state = state;

            if (nameToStateBundle.Count == 1 && !startState.hasState)
            {
                SetStartState(name);
            }
        }
        /// <summary>
		/// Initialises a transition, i.e. sets its fsm attribute, and then calls its Init method.
		/// </summary>
		/// <param name="transition"></param>
		private void InitTransition(TransitionBase<TStateId> transition)
        {
            transition.owner = this;
            transition.Init();
        }
        /// <summary>
        /// Adds a new transition between two states.
        /// </summary>
        /// <param name="transition">The transition instance</param>
        public void AddTransition(TransitionBase<TStateId> transition)
        {
            InitTransition(transition);

            StateBundle bundle = GetOrCreateStateBundle(transition.from);
            bundle.AddTransition(transition);
        }
        /// <summary>
		/// Adds a new transition that can happen from any possible state
		/// </summary>
		/// <param name="transition">The transition instance; The "from" field can be
		/// left empty, as it has no meaning in this context.</param>
		public void AddTransitionFromAny(TransitionBase<TStateId> transition)
        {
            InitTransition(transition);

            transitionsFromAny.Add(transition);
        }
        /// <summary>
		/// Adds a new trigger transition between two states that is only checked
		/// when the specified trigger is activated.
		/// </summary>
		/// <param name="trigger">The name / identifier of the trigger</param>
		/// <param name="transition">The transition instance, e.g. Transition, TransitionAfter, ...</param>
		public void AddTriggerTransition(TEvent trigger, TransitionBase<TStateId> transition)
        {
            InitTransition(transition);

            StateBundle bundle = GetOrCreateStateBundle(transition.from);
            bundle.AddTriggerTransition(trigger, transition);
        }
        /// <summary>
		/// Adds a new trigger transition that can happen from any possible state, but is only
		/// checked when the specified trigger is activated.
		/// </summary>
		/// <param name="trigger">The name / identifier of the trigger</param>
		/// <param name="transition">The transition instance; The "from" field can be
		/// left empty, as it has no meaning in this context.</param>
		public void AddTriggerTransitionFromAny(TEvent trigger, TransitionBase<TStateId> transition)
        {
            InitTransition(transition);

            List<TransitionBase<TStateId>> transitionsOfTrigger;

            if (!triggerTransitionsFromAny.TryGetValue(trigger, out transitionsOfTrigger))
            {
                transitionsOfTrigger = new List<TransitionBase<TStateId>>();
                triggerTransitionsFromAny.Add(trigger, transitionsOfTrigger);
            }

            transitionsOfTrigger.Add(transition);
        }
        /// <summary>
		/// Activates the specified trigger, checking all targeted trigger transitions to see whether
		/// a transition should occur.
        /// 注意这里的逻辑是如果从FromAny就无法自己切换到自己，如果是普通的trigger transition就可以切换到自己
        /// 这个方法也会考虑Trigger的transition条件是否满足
		/// </summary>
		/// <param name="trigger">The name / identifier of the trigger</param>
		/// <returns>True when a transition occurred, otherwise false</returns>
		private bool TryTrigger(TEvent trigger)
        {
            EnsureIsInitializedFor("Checking all trigger transitions of the active state");

            List<TransitionBase<TStateId>> triggerTransitions;

            if (triggerTransitionsFromAny.TryGetValue(trigger, out triggerTransitions))
            {
                for (int i = 0; i < triggerTransitions.Count; i++)
                {
                    TransitionBase<TStateId> transition = triggerTransitions[i];

                    if (EqualityComparer<TStateId>.Default.Equals(transition.to, activeState.Name))
                        continue;

                    if (TryTransition(transition))
                        return true;
                }
            }

            if (activeTriggerTransitions.TryGetValue(trigger, out triggerTransitions))
            {
                for (int i = 0; i < triggerTransitions.Count; i++)
                {
                    TransitionBase<TStateId> transition = triggerTransitions[i];

                    if (TryTransition(transition))
                        return true;
                }
            }

            return false;
        }
        /// <summary>
		/// Activates the specified trigger in all active states of the hierarchy, checking all targeted
		/// trigger transitions to see whether a transition should occur.
        /// 如果当前状态机的activeState是一个子状态机，那么这个方法会递归调用子状态机的Trigger方法，
        /// 直到最底层的activeState不是一个状态机或者Trigger达不到条件为止
		/// </summary>
		/// <param name="trigger">The name / identifier of the trigger</param>
		public void Trigger(TEvent trigger)
        {
            // If a transition occurs, then the trigger should not be activated
            // in the new active state, that the state machine just switched to.
            if (TryTrigger(trigger)) return;

            (activeState as ITriggerable<TEvent>)?.Trigger(trigger);
        }
        /// <summary>
		/// Only activates the specified trigger locally in this state machine.
        /// 只会在当前状态机中激活指定的触发器，而不会递归调用子状态机的Trigger方法
		/// </summary>
		/// <param name="trigger">The name / identifier of the trigger</param>
		public void TriggerLocally(TEvent trigger)
        {
            TryTrigger(trigger);
        }
        public IState<TStateId> GetState(TStateId name)
        {
            StateBundle bundle;

            if (!nameToStateBundle.TryGetValue(name, out bundle) || bundle.state == null)
            {
                throw new Exceptions.StateNotFoundException<TStateId>(name, "Getting a state");
            }

            return bundle.state;
        }
        
        private void EnsureIsInitializedFor(string context)
        {
            if (activeState == null)
                throw new Exceptions.StateMachineNotInitializedException(context);
        }
    }
    #endregion
#region Overloaded classes
    // Overloaded classes to allow for an easier usage of the StateMachine for common cases.
    // E.g. new StateMachine() instead of new StateMachine<string, string, string>()

    public class StateMachine<TStateId, TEvent> : StateMachine<TStateId, TStateId, TEvent>
    {
        public StateMachine(bool needsExitTime = true) : base(needsExitTime)
        {
        }
    }

    public class StateMachine<TStateId> : StateMachine<TStateId, TStateId, string>
    {
        public StateMachine(bool needsExitTime = true) : base(needsExitTime)
        {
        }
    }

    public class StateMachine : StateMachine<string, string, string>
    {
        public StateMachine(bool needsExitTime = true) : base(needsExitTime)
        {
        }
    }
#endregion
}
