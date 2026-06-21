using UnityEngine;
using System;
using System.Collections.Generic;
namespace BTrees
{
	/// <summary>
	/// 复合节点，包含一个或多个子节点，执行逻辑由子类实现
	/// </summary>
	public abstract class BTComposite : MonoBehaviour, IBTNode
	{
		public List<BTComposite> needAbortLowPriority { get; private set; } = new List<BTComposite>();
		public List<BTCondition> conditions { get; private set; } = new List<BTCondition>();

		public virtual void Awake()
		{
			children.Clear();
			children.AddRange(GetComponentsInChildren<IBTNode>(true));
			foreach (IBTNode node in children)
			{
				if (node is BTComposite composite)
				{
					if (composite.abortType == AbortType.LowPriority || composite.abortType == AbortType.Both)
					{
						needAbortLowPriority.Add(composite);
					}
				}
				if (node is BTCondition condition)
				{
					conditions.Add(condition);
				}
			}
		}
		protected List<IBTNode> children = new List<IBTNode>();
		protected int currentIndex = 0;
		public AbortType abortType = AbortType.None;
		
		//作为需要打断低优先级的子节点调用,返回如果打断的话是什么情况
		public abstract AbortResult AbortLowerResult();
		public BTState Tick()
		{
			if (needAbortLowPriority.Count != 0)
			{
				for (int i = 0; i < currentIndex; i++)
				{
					if (children[i] is BTComposite composite
					&& (composite.abortType == AbortType.LowPriority || composite.abortType == AbortType.Both))
					{
						//GD.Print("Sequence Check Abort Lower Priority Composite: " + composite.Name);
						switch (composite.AbortLowerResult())
						{
							case AbortResult.None:
								continue;
							case AbortResult.ReturnSuccess:
								children[currentIndex].Abort();
								currentIndex = 0;
								return BTState.Success;
							case AbortResult.ReturnFailure:
								children[currentIndex].Abort();
								currentIndex = 0;
								return BTState.Failure;
							case AbortResult.OccupyBranch:
								//打断后占领分支，下次tick从被打断的节点执行，被打断的节点不重复判断
								children[currentIndex].Abort();
								currentIndex = i;
								break;
						}
					}
				}
			}
			switch (abortType)
			{
				case AbortType.None:
					return TickStateful();
				case AbortType.Self:
					return TickAbortSelf();
				case AbortType.LowPriority:
					return TickStateful();
				case AbortType.Both:
					return TickAbortSelf();
				default:
					return TickStateful();
			}
		}
		protected abstract BTState TickStateful();
		protected abstract BTState TickAbortSelf();

		public string GetRunningLeafNodeName()
		{
			if (currentIndex >= 0 && currentIndex < children.Count)
				return children[currentIndex].GetRunningLeafNodeName();

			return null;
		}

		public virtual void Abort()
		{
			if (currentIndex >= 0 && currentIndex < children.Count)
			{
				children[currentIndex].Abort();
			}
			currentIndex = 0;
		}

	}
	public enum AbortResult
	{
		None,
		ReturnSuccess,
		ReturnFailure,
		OccupyBranch
	}
}