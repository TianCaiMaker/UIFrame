
namespace BTrees.Composites
{
	/// <summary>
	/// 选择节点，所有子节点依次执行，直到有一个成功或者正在执行的节点
	/// </summary>
	public class Selector : BTComposite
	{
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
					return BTState.Success;
				}

				currentIndex++;
			}

			currentIndex = 0;
			return BTState.Failure;
		}

		protected override BTState TickAbortSelf()
		{
			for (int i = 0; i < currentIndex; i++)
			{
				if(children[i] is BTCondition condition)
				{
					BTState result = children[i].Tick();
					if(result == BTState.Success)
					{
						children[currentIndex].Abort();
						currentIndex = 0;
						return BTState.Success;
					}
				}
			}
			return TickStateful();
		}
		public override AbortResult AbortLowerResult()
		{
			foreach(BTCondition condition in conditions)
			{
				BTState result = condition.Tick();
				if (result == BTState.Success)
				{
					return AbortResult.ReturnSuccess;
				}
			}
			return AbortResult.None;
		}
	}
}