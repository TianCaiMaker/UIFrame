using System;
using BTrees;
namespace BHTrees.Actions
{
	public class Idle : BTAction
	{
		protected override BTState OnTick()
		{
			//Debug.Log("Idle...");
			return BTState.Running;
		}
	}

}
