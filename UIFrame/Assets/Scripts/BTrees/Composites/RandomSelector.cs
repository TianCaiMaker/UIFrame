using System;
using BTrees;
using System.Collections.Generic;
namespace BTrees.Composites
{
	/// <summary>
	/// 随机选择节点，所有子节点随机顺序执行，直到有一个成功或者正在执行的节点
	/// </summary>
	public class RandomSelector : Selector
	{
		public override void Awake()
		{
			base.Awake();
			RandomizeChildren();
		}
		private void RandomizeChildren()
		{
			Random random = new Random();
			int n = children.Count;
			for (int i = 0; i < n - 1; i++)
			{
				int j = random.Next(i, n);
				// 交换 children[i] 和 children[j]
				var temp = children[i];
				children[i] = children[j];
				children[j] = temp;
			}
		}
		protected override BTState TickStateful()
		{
			while (currentIndex < children.Count)
			{
				BTState result = children[currentIndex].Tick();

				if (result == BTState.Running)
					return BTState.Running;

				if (result == BTState.Success)
				{
					currentIndex = 0;
					RandomizeChildren();
					return BTState.Success;
				}

				currentIndex++;
			}

			currentIndex = 0;
			RandomizeChildren();
			return BTState.Failure;
		}

		protected override BTState TickAbortSelf()
		{
			for (int i = 0; i < children.Count; i++)
			{
				if(children[i] is BTCondition condition)
				{
					BTState result = children[i].Tick();
					if(result == BTState.Success)
					{
						children[currentIndex].Abort();
						currentIndex = 0;
						RandomizeChildren();
						return BTState.Success;
					}
				}
			}
			return TickStateful();
		}
		public override void Abort()
		{
			if (currentIndex >= 0 && currentIndex < children.Count)
			{
				children[currentIndex].Abort();
			}
			RandomizeChildren();
			currentIndex = 0;
		}
	}
}
