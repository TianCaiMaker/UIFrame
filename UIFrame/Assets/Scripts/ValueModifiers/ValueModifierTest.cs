using Attributes;
using UnityEngine;
using System;
using Modifier.Test;
namespace Modifier.Test
{

	public enum AttributeName
	{
		MaxHealth,
		MoveSpeed,
		Attack,
		Defense,
	}
}
public class ValueModifierTest : AttributeValue<AttributeName, ModifierSourceType, ModifierMultiplierType>
{
	public override AttributeName attributeName => AttributeName.Attack;

	public void Awake()
	{
		// 订阅变更事件，打印最新值
		this.ValueChanged += v => Debug.Log("ValueChanged:" + v);

		// 同 multiplierType 的两个 buff（BaseAdd）
		var b1 = new AttributeModifier<AttributeName, ModifierSourceType, ModifierMultiplierType>
		{
			buffName = "BaseAdd1",
			priority = 1,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Equipment,
			multiplierType = ModifierMultiplierType.Base,
			addBaseValue = 10f,
			additivePercents = 0.15f,
			finalAddValue = 1f
		};

		var b2 = new AttributeModifier<AttributeName, ModifierSourceType, ModifierMultiplierType>
		{
			buffName = "BaseAdd2",
			priority = 2,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Equipment,
			multiplierType = ModifierMultiplierType.Base,
			addBaseValue = 5f,
			additivePercents = 0.15f,
			finalAddValue = 2f
		};

		// 不同 multiplierType：BasePercent
		var b3 = new AttributeModifier<AttributeName, ModifierSourceType, ModifierMultiplierType>
		{
			buffName = "BasePercent1",
			priority = 1,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Skill,
			multiplierType = ModifierMultiplierType.Add,
			addBaseValue = 0f,
			additivePercents = 0.1f, // 10%
			finalAddValue = 3f
		};

		// 不同 multiplierType：AdditivePercent
		var b4 = new AttributeModifier<AttributeName, ModifierSourceType, ModifierMultiplierType>
		{
			buffName = "AdditivePercent1",
			priority = 1,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Potion,
			multiplierType = ModifierMultiplierType.Add,
			addBaseValue = 20f,
			additivePercents = 0.2f, // 20%
			finalAddValue = 4f
		};

		// 添加并观察事件输出
		AddBuff(b1);
		AddBuff(b2);
		AddBuff(b3);
		AddBuff(b4);
		RemoveBuff(b1);
		RemoveBuff(b2);
		// 你也可以试试移除
		// RemoveBuff(b2);
	}

}
