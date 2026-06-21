using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BTrees.Composites
{
	/// <summary>
	/// 顺序节点，所有子节点依次执行，直到有一个失败或者正在执行的节点
	/// </summary>
	public class Sequence : BTComposite
	{
		protected override BTState TickStateful()
		{
			while (currentIndex < children.Count)
			{
				BTState result = children[currentIndex].Tick();
				
				if (result == BTState.Running)
					return BTState.Running;

				if (result == BTState.Failure)
				{
					currentIndex = 0;
					return BTState.Failure;
				}

				currentIndex++;
			}
			//GD.Print("Sequence Success");
			currentIndex = 0;
			return BTState.Success;
		}
		protected override BTState TickAbortSelf()
		{
			for (int i = 0; i < currentIndex; i++)
			{
				if (children[i] is BTCondition condition)
				{
					BTState result = children[i].Tick();
					//GD.Print("Sequence Abort Check Condition Result: " + result);
					if (result == BTState.Failure)
					{
						children[currentIndex].Abort();
						currentIndex = 0;
						return BTState.Failure;
					}
				}
			}
			return TickStateful();
		}
		public override AbortResult AbortLowerResult()
		{
			if (children[0] is BTCondition condition)
			{

				if (condition.Predicate())
				{
					//打断后占领分支，下次tick从第二个节点执行，第一个节点不重复判断
					currentIndex = 1;
					return AbortResult.OccupyBranch;
				}
				else
				{
					return AbortResult.None;
				}
			}
			return AbortResult.None;
		}
	}
}