using System.Linq;
using GAS.General;
using UnityEngine;

namespace GAS.Runtime
{
    public abstract class ModifierMagnitudeCalculation : ScriptableObject
    {
        protected const int WIDTH_LABEL = 70;

        // [TitleGroup("Base")]
        // [HorizontalGroup("Base/H1", width: 1 - 0.618f)]
        // [TabGroup("Base/H1/V1", "Summary", SdfIconType.InfoSquareFill, TextColor = "#0BFFC5", Order = 1)]
        // [HideLabel]
        // [MultiLineProperty(10)]
        public string Description;

        public abstract float CalculateMagnitude(GameplayEffectSpec spec, float modifierMagnitude);

#if UNITY_EDITOR
        private void OnValidate()
        {
            // if(Application.isPlaying) return;
            // EditorUtility.SetDirty(this);
            // AssetDatabase.SaveAssets();
            // AssetDatabase.Refresh();
        }
#endif
    }
}