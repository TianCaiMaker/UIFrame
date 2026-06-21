
using System;
namespace Attributes
{
	/// <summary>
	/// 表示一个数值增益（Buff），可以影响角色的属性。
	/// </summary>
	/// <typeparam name="TValue">数值类型，是攻击力还是防御力</typeparam>
	/// <typeparam name="TSource">来源类型，是装备还是技能</typeparam>
	/// <typeparam name="TMultiplier">乘区类型</typeparam>
	public class AttributeModifier<TValue,TSource,TMultiplier>
	where TValue : Enum 
	where TSource : Enum
	where TMultiplier : Enum
	{
		public string buffName;
		public int priority;
		public TValue valueType;
		public TSource sourceType;
		public TMultiplier multiplierType;
		public float addBaseValue;
		public float additivePercents;
		public float finalAddValue;
	}
}