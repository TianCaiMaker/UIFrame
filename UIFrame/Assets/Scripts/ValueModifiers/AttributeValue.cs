using System;
using System.Collections.Generic;
using UnityEngine;
namespace Attributes
{
	public abstract class AttributeValue<TValue, TSource> : MonoBehaviour
	where TValue : Enum
	where TSource : Enum
	{
		abstract public TValue attributeName { get; }
		// 按 buff.priority 到 buff 列表的映射（按 priority 排序，priority 小的先计算）
		SortedList<int, List<AttributeModifier<TValue, TSource>>> Muldifiers = new();
		// 若为 false，则不允许 GetValue 返回负数（默认 false）
		public bool allowNegative = false;
		bool isDirty = true;
		float cachedValue = 0f;

		public event Action<float> ValueChanged;

		public void AddBuff(AttributeModifier<TValue, TSource> buff)
		{
			if (buff == null) return;
			int key = buff.priority;
			if (!Muldifiers.TryGetValue(key, out var list))
			{
				list = new List<AttributeModifier<TValue, TSource>>();
				Muldifiers[key] = list;
			}
			// 在同一 multiplierType 桶中按插入顺序添加（multiplierType 已作为桶的排序键）
			list.Add(buff);
			isDirty = true;
			ValueChanged?.Invoke(GetValue);
		}

		public bool RemoveBuff(AttributeModifier<TValue, TSource> buff)
		{
			if (buff == null) return false;
			int key = buff.priority;
			if (Muldifiers.TryGetValue(key, out var list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (ReferenceEquals(list[i], buff))
					{
						list.RemoveAt(i);
						if (list.Count == 0) Muldifiers.Remove(key);
						isDirty = true;
						ValueChanged?.Invoke(GetValue);
						return true;
					}
				}
			}
			// 兜底：在所有桶中查找（防止 priority 字段不匹配或外部引用问题）
			for (int k = 0; k < Muldifiers.Count; k++)
			{
				var list2 = Muldifiers.Values[k];
				for (int i = 0; i < list2.Count; i++)
				{
					if (ReferenceEquals(list2[i], buff))
					{
						list2.RemoveAt(i);
						if (list2.Count == 0) Muldifiers.RemoveAt(k);
						isDirty = true;
						ValueChanged?.Invoke(GetValue);
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 获取属性值,计算顺序：先加基值，再按 `priority` 值从小到大（小的先）顺序计算
		/// 对于相同 `priority` 的 buff，保持插入顺序；每个 priority 桶的计算逻辑不变：
		/// 先汇总该桶的 `additivePercents` 与 `finalAddValue`，再按 result = result * (1 + additiveSum) + finalAddSum
		/// </summary>
		public float GetValue
		{
			get
			{
				if (isDirty)
				{
					float result = 0f;

					for (int index = 0; index < Muldifiers.Count; index++)
					{
						var list = Muldifiers.Values[index];
						for (int i = 0; i < list.Count; i++)
						{
							result += list[i].AddBaseValue;
						}
					}

					// 按 priority 值升序计算（priority 小的先计算，数字小的先计算）
					for (int index = 0; index < Muldifiers.Count; index++)
					{
						var list = Muldifiers.Values[index];
						float additiveSum = 0f;
						float finalAddSum = 0f;
						for (int i = 0; i < list.Count; i++)
						{
							additiveSum += list[i].AdditivePercents;
							finalAddSum += list[i].FinalAddValue;
						}
						result = result * (1f + additiveSum) + finalAddSum;
					}

					cachedValue = result;
					isDirty = false;
				}

				if (!allowNegative && cachedValue < 0f) return 0f;
				return cachedValue;
			}
		}
	}//class
}