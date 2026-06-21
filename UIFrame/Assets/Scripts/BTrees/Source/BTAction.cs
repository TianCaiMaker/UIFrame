using System;
using UnityEngine;
namespace BTrees
{
	/// <summary>
	/// 行为树动作节点
	/// </summary>
	public abstract class BTAction : MonoBehaviour, IBTNode
	{
		private bool isRunning = false;
		protected virtual void OnEnter() { }
		protected virtual void OnExit() { }
		protected virtual void OnAbort() { OnExit(); }
		protected abstract BTState OnTick();
		public BTState Tick()
		{
			if (!isRunning)
			{
				OnEnter();
				isRunning = true;
			}

			BTState state = OnTick();

			if (state != BTState.Running)
			{
				OnExit();
				isRunning = false;
			}

			return state;
		}

		public string GetRunningLeafNodeName()
		{
			return isRunning ? name : null;
		}

		public void Abort()
		{
			if (!isRunning)
			{
				return;
			}

			OnAbort();
			isRunning = false;
		}
	}
}
