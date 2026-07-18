
namespace StateMachines
{
	/// <summary>
	/// The base class of all states
	/// </summary>
	public interface IState<TStateId>
	{
		public bool IsAutoChange { get;}
		public TStateId Name { get; }
		public IStateMachine<TStateId> Owner { get; set; }

		/// <summary>
		/// Initialises a new instance of the IState class
		/// </summary>
		/// <param name="isAutoChange">Determines if the state is allowed to instantly
		/// 	exit on a transition (true), or if the state machine should wait until
		/// 	the state is ready for a state change (false)</param>

		/// <summary>
		/// Called to initialise the state, after values like name, mono and fsm have been set
		/// </summary>
		public void Init();

		/// <summary>
		/// Called when the state machine transitions to this state (enters this state)
		/// </summary>
		public void OnEnter();

		/// <summary>
		/// Called while this state is active
		/// </summary>
		public void OnLogic();

		/// <summary>
		/// Called when the state machine transitions from this state to another state (exits this state)
		/// </summary>
		public void OnExit();

		/// <summary>
		/// (Only if IsAutoChange is true):
		/// 	Called when a state transition from this state to another state should happen.
		/// 	If it can exit, it should call fsm.StateCanExit()
		/// 	and if it can not exit right now, it should call fsm.StateCanExit() later in OnLogic().
		/// </summary>
		public void OnExitRequest();
	}

	public interface StateBase : IState<string>
	{
	}
}
