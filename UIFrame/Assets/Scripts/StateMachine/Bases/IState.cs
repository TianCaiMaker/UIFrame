
namespace StateMachines
{
	/// <summary>
	/// The base class of all states
	/// </summary>
	public interface IState<TStateId>
	{
		public bool NeedsExitTime { get;}
		public TStateId Name { get;}
		public IStateMachine<TStateId> Owner { get; set; }

		/// <summary>
		/// Initialises a new instance of the IState class
		/// </summary>
		/// <param name="NeedsExitTime">Determins if the state is allowed to instantly
		/// 	exit on a transition (false), or if the state machine should wait until
		/// 	the state is ready for a state change (true)
		/// 	这个状态是否需要退出时机，如果是false，就是在可以退出状态的时候立即退出，
		/// 	如果是true，就是在状态机准备好退出状态的时候才退出
		/// </param>

		/// <summary>
		/// Called to initialise the state, after values like name, mono and fsm have been set
		/// 一般在加入状态的时候调用
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

	public interface IState : IState<string>
	{
	}
}
