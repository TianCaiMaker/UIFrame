using Attributes;
using UnityEngine;
using System;
using Modifier.Test;
namespace Modifier.Test
{
	public enum ModifierSourceType
	{
		Equipment,
		Skill,
		Potion,
	}
	public enum AttributeName
	{
		MaxHealth,
		MoveSpeed,
		Attack,
		Defense,
	}
}
public class ValueModifierTest : AttributeValue<AttributeName, ModifierSourceType>
{
	public override AttributeName attributeName => AttributeName.Attack;

	public void Awake()
	{
		// 订阅变更事件，打印最新值
		this.ValueChanged += v => Debug.Log("ValueChanged:" + v);

		// 同 multiplierType 的两个 buff（BaseAdd）
		var b1 = new AttributeModifier<AttributeName, ModifierSourceType>
		{
			buffName = "BaseAdd1",
			priority = 0,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Equipment,
			AddBaseValue = 10f,
			AdditivePercents = 0.15f,
			FinalAddValue = 1f
		};

		var b2 = new AttributeModifier<AttributeName, ModifierSourceType>
		{
			buffName = "BaseAdd2",
			priority = 2,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Equipment,
			AddBaseValue = 5f,
			AdditivePercents = 0.15f,
			FinalAddValue = 2f
		};

		// 不同 multiplierType：BasePercent
		var b3 = new AttributeModifier<AttributeName, ModifierSourceType>
		{
			buffName = "BasePercent1",
			priority = 1,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Skill,
			AddBaseValue = 0f,
			AdditivePercents = 0.1f, // 10%
			FinalAddValue = 3f
		};

		// 不同 multiplierType：AdditivePercent
		var b4 = new AttributeModifier<AttributeName, ModifierSourceType>
		{
			buffName = "AdditivePercent1",
			priority = 1,
			valueType = AttributeName.Attack,
			sourceType = ModifierSourceType.Potion,
			AddBaseValue = 20f,
			AdditivePercents = 0.2f, // 20%
			FinalAddValue = 4f
		};

		// 添加并观察事件输出
		AddBuff(b1);
		AddBuff(b2);
		AddBuff(b3);
		AddBuff(b4);
		// 你也可以试试移除
		// RemoveBuff(b2);
	}

}
