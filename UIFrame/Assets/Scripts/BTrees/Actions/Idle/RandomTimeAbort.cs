using System;
using BTrees;
using UnityEngine;
namespace BHTrees.Actions
{
	public partial class RandomTimeAbort : BTCondition
	{
		public bool abortSelector = true;
		public float minTime = 3f;
		public float maxTime = 5f;
		float currentTime = 0f;
		System.Random rand = new System.Random();
		TimeSystem.Timer timer = new TimeSystem.Timer();
		public void Awake()
		{
			ResetTimer();
		}

		public override bool Predicate()
		{

			if (timer < currentTime)
			{
				return false==abortSelector;
			}
			else
			{
				ResetTimer();
				Debug.Log("Random Time Abort Triggered: " + currentTime);
				return true==abortSelector;
				
			}
		}
		private void ResetTimer()
		{
			timer.Reset();
			currentTime = (float)(rand.NextDouble() * (maxTime - minTime) + minTime);
		}
	}
}
