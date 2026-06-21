namespace Attributes
{
	public enum ModifierSourceType
	{
		Equipment,
		Skill,
		Potion,
	}
	public enum ModifierMultiplierType
	{
		Base,       // 直接加在基础值上的增益（如装备提供的固定加成）
		Add,   // 以基础值为基准的百分比增益（如某些技能提供的百分比加成）
		AdditivePercent, // 以当前值为基准的百分比增益，按优先级叠加（如持续效果的增益）
	}
}
