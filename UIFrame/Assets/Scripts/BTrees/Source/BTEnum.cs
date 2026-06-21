using System;
namespace BTrees
{
	/// <summary>
	/// 行为树枚举
	/// </summary>
	public enum BTState
	{
		Success,
		Failure,
		Running
	}
	public enum AbortType
	{
		None,
		Self,
		LowPriority,
		Both
	}
}