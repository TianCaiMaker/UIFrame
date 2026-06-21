using System;
using System.Collections.Generic;
using UnityEngine;
namespace BTrees
{
	/// <summary>
	/// 行为树
	/// </summary>
	public class BTree : MonoBehaviour
	{
		public bool enableDebug = false;
		public float tickInterval = 0.3f;

		private IBTNode child;
		private double _accumulator = 0.0;

		public void Awake()
		{
			foreach (Transform child in transform)
			{
				if (child.GetComponent<IBTNode>() is IBTNode node)
				{
					this.child = node;
					break;
				}
			}
		}

		public void Update()
		{
			_accumulator += Time.deltaTime;
			// 支持累积多个间隔（如果 frame 过长）
			while (_accumulator >= tickInterval)
			{
				_accumulator -= tickInterval;
				TickOnce();
			}
		}
		// 这里的策略是每次 Tick 都从头开始执行所有子节点，直到遇到 Running 或 Success
		//其实行为树根节点相当与一个 Selector
		//逻辑有问题，没有打断，节点不能正确exit。
		private void TickOnce()
		{
			if (child == null)
			{
				return;
			}

			BTState res = child.Tick();
			// 常见策略：一旦子节点返回 Running 或 Success 就停止本次 Tick
			if (res == BTState.Running)
			{
				if (enableDebug)
				{
					Debug.Log(name + " BTree Tick " + child.GetRunningLeafNodeName());
				}
				return;
			}
			if (res == BTState.Success)
			{
				return;
			}
		}
	}
}
