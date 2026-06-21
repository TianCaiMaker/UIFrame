namespace BTrees
{
	/// <summary>
	/// 行为树节点接口
	/// </summary>
	public interface IBTNode
	{
		//不用添加Init方法，因为继承Node的类可以直接使用Ready方法进行初始化
		public BTState Tick();
		public string GetRunningLeafNodeName();
		public void Abort();
	}
}