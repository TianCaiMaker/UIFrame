using System;
using UnityEngine;
namespace BTrees
{
	/// <summary>
	/// 条件节点，判断条件是否满足
	/// </summary>
	public abstract class BTCondition : MonoBehaviour, IBTNode
	{
		public abstract bool Predicate();

		public BTState Tick()
		{
			return Predicate() ? BTState.Success : BTState.Failure;
		}

		public string GetRunningLeafNodeName()
		{
			return null;
		}

		public void Abort() { }
	}
}