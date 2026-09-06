using System.Linq;
using GAS.General;
using UnityEngine;

namespace GAS.Runtime
{
    public abstract class GameplayCue : ScriptableObject
    {
        protected const int WIDTH_LABEL = 70;

        // [TitleGroup("Base")]
        // [HorizontalGroup("Base/H1")]
        // [TabGroup("Base/H1/V1", "Summary", SdfIconType.InfoSquareFill, TextColor = "#0BFFC5", Order = 1)]
        // [HideLabel]
        // [MultiLineProperty(10)]
        public string Description;
        // Tags
        // [TabGroup("Base/H1/V3", "Tags", SdfIconType.TagsFill, TextColor = "#45B1FF", Order = 3)]
        // [ListDrawerSettings(ShowFoldout = true, ShowItemCount = false)]
        // [ValueDropdown("@ValueDropdownHelper.GameplayTagChoices", IsUniqueList = true, HideChildProperties = true)]
        // [LabelText("RequiredTags - 持有所有标签才可触发")]
        public GameplayTag[] RequiredTags;

        // [TabGroup("Base/H1/V3", "Tags")]
        // [ListDrawerSettings(ShowFoldout = true, ShowItemCount = false)]
        // [ValueDropdown("@ValueDropdownHelper.GameplayTagChoices", IsUniqueList = true, HideChildProperties = true)]
        // [LabelText("ImmunityTags - 持有任意标签不可触发")]
        public GameplayTag[] ImmunityTags;

        public virtual bool Triggerable(AbilitySystemComponent owner)
        {
            if (owner == null) return false;
            // 持有【所有】RequiredTags才可触发
            if (!owner.HasAllTags(new GameplayTagSet(RequiredTags)))
                return false;

            // 持有【任意】ImmunityTags不可触发
            if (owner.HasAnyTags(new GameplayTagSet(ImmunityTags)))
                return false;

            return true;
        }
    }

    public abstract class GameplayCue<T> : GameplayCue where T : GameplayCueSpec
    {
        public abstract T CreateSpec(GameplayCueParameters parameters);
    }
}