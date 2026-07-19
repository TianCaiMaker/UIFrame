
using System;
namespace Attributes
{
	/// <summary>
	/// 表示一个数值增益（Buff），可以影响角色的属性。
	/// </summary>
	/// <typeparam name="TValue">数值类型，是攻击力还是防御力</typeparam>
	/// <typeparam name="TSource">来源类型，是装备还是技能</typeparam>
	public class AttributeModifier<TValue,TSource>
	where TValue : Enum 
	where TSource : Enum
	{
		public string buffName;
		public int priority;
		public TValue valueType;
		public TSource sourceType;
		private float addBaseValue;
		private float additivePercents;
		private float finalAddValue;
		public virtual float AddBaseValue
		{
			get
			{
				return addBaseValue;
			}
			set
			{
				addBaseValue = value;
			}
		}
		public virtual float AdditivePercents
		{
			get
			{
				return additivePercents;
			}
			set
			{
				additivePercents = value;
			}
		}
		public virtual float FinalAddValue
		{
			get
			{
				return finalAddValue;
			}
			set
			{
				finalAddValue = value;
			}
		}
	}
}