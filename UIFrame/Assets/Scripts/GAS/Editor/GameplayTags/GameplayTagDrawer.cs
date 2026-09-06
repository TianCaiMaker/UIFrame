using GAS.Runtime;
using UnityEditor;
using UnityEngine;

namespace GAS.Editor
{
    [CustomPropertyDrawer(typeof(GameplayTag))]
    internal class GameplayTagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var fieldRect = EditorGUI.PrefixLabel(position, label);
            var selectRect = new Rect(fieldRect.x, fieldRect.y, Mathf.Max(120f, fieldRect.width - 86f), fieldRect.height);
            var clearRect = new Rect(fieldRect.xMax - 56f, fieldRect.y, 26f, fieldRect.height);
            var manageRect = new Rect(fieldRect.xMax - 28f, fieldRect.y, 26f, fieldRect.height);

            var currentName = property.FindPropertyRelative("_name").stringValue;
            var buttonText = string.IsNullOrEmpty(currentName) ? "Select GameplayTag" : currentName;

            if (GUI.Button(selectRect, buttonText, EditorStyles.popup))
            {
                GameplayTagPickerWindow.ShowWindow(selectRect, property.serializedObject.targetObject, property.propertyPath);
            }

            using (new EditorGUI.DisabledScope(string.IsNullOrEmpty(currentName)))
            {
                if (GUI.Button(clearRect, "X"))
                {
                    GameplayTagEditorUtility.AssignTag(property, string.Empty);
                    property.serializedObject.ApplyModifiedProperties();
                }
            }

            if (GUI.Button(manageRect, EditorGUIUtility.IconContent("d_SettingsIcon", "Open GameplayTag manager"),
                    EditorStyles.iconButton))
            {
                TagManager.ShowWindow();
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}