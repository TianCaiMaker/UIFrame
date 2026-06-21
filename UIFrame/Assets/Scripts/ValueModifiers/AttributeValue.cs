using System;
using System.Collections.Generic;
using UnityEngine;
namespace Attributes
{
	public abstract class AttributeValue<TValue, TSource, TMultiplier> : MonoBehaviour
	where TValue : Enum
	where TSource : Enum
	where TMultiplier : Enum
	{
		abstract public TValue attributeName { get; }
		// 按 multiplierType(int) 到 buff 列表的映射（按 multiplierType 排序）
		// 使用 List 替代 LinkedList，以便更好地利用 CPU 缓存，读取更快
		SortedList<int, List<AttributeModifier<TValue, TSource, TMultiplier>>> buffs = new();
		// 若为 false，则不允许 GetValue 返回负数（默认 false）
		public bool allowNegative = false;
		bool isDirty = true;
		float cachedValue = 0f;

		public event Action<float> ValueChanged;

		public void AddBuff(AttributeModifier<TValue, TSource, TMultiplier> buff)
		{
			if (buff == null) return;
			int key = Convert.ToInt32(buff.multiplierType);
			if (!buffs.TryGetValue(key, out var list))
			{
				list = new List<AttributeModifier<TValue, TSource, TMultiplier>>();
				buffs[key] = list;
			}
			// 在同一 multiplierType 桶中按插入顺序添加（multiplierType 已作为桶的排序键）
			list.Add(buff);
			isDirty = true;
			ValueChanged?.Invoke(GetValue);
		}

		public bool RemoveBuff(AttributeModifier<TValue, TSource, TMultiplier> buff)
		{
			if (buff == null) return false;
			int key = Convert.ToInt32(buff.multiplierType);
			if (buffs.TryGetValue(key, out var list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (ReferenceEquals(list[i], buff))
					{
						list.RemoveAt(i);
						if (list.Count == 0) buffs.Remove(key);
						isDirty = true;
						ValueChanged?.Invoke(GetValue);
						return true;
					}
				}
			}
			// 兜底：在所有桶中查找（防止 multiplierType 字段不匹配的情况）
			for (int k = 0; k < buffs.Count; k++)
			{
				var list2 = buffs.Values[k];
				for (int i = 0; i < list2.Count; i++)
				{
					if (ReferenceEquals(list2[i], buff))
					{
						list2.RemoveAt(i);
						if (list2.Count == 0) buffs.RemoveAt(k);
						isDirty = true;
						ValueChanged?.Invoke(GetValue);
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 获取属性值,计算顺序：先加基值，再按 multiplierType 小的优先的顺序
		/// 依次乘以 基础区总和*[(1 + additivePercents)+finalAddValue]再乘下一个乘区
		/// </summary>
		public float GetValue
		{
			get
			{
				if (isDirty)
				{
					float result = 0f;

					for (int index = 0; index < buffs.Count; index++)
					{
						var list = buffs.Values[index];
						for (int i = 0; i < list.Count; i++)
						{
							result += list[i].addBaseValue;
						}
					}

					// 按 multiplierType int 值升序计算（数字小的先计算）
					for (int index = 0; index < buffs.Count; index++)
					{
						var list = buffs.Values[index];
						float additiveSum = 0f;
						float finalAddSum = 0f;
						for (int i = 0; i < list.Count; i++)
						{
							additiveSum += list[i].additivePercents;
							finalAddSum += list[i].finalAddValue;
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