using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GAS.Editor
{
    internal class GameplayTagPickerWindow : EditorWindow
    {
        private const float RowHeight = 24f;
        private const float IndentWidth = 16f;
        private const float DefaultWidth = 420f;
        private const float DefaultHeight = 520f;
        private const float ExtraContentWidth = 96f;

        private Object _targetObject;
        private string _propertyPath;
        private Vector2 _scrollPosition;
        private readonly Dictionary<string, bool> _expandedStates = new Dictionary<string, bool>();

        public static void ShowWindow(Rect triggerRect, Object targetObject, string propertyPath)
        {
            var window = CreateInstance<GameplayTagPickerWindow>();
            window.titleContent = new GUIContent("GameplayTag Picker");
            window._targetObject = targetObject;
            window._propertyPath = propertyPath;
            window.minSize = new Vector2(360f, 320f);

            var screenRect = GUIUtility.GUIToScreenRect(triggerRect);
            window.position = new Rect(screenRect.x, screenRect.yMax + 2f, DefaultWidth, DefaultHeight);
            window.ShowUtility();
            window.Focus();
        }

        private void OnGUI()
        {
            var database = GameplayTagEditorUtility.GetOrCreateDatabase();
            var roots = GameplayTagEditorUtility.GetRoots(database);
            var contentWidth = Mathf.Max(position.width - 32f, CalculateContentWidth(roots, string.Empty, 0));

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("Select GameplayTag", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Manage", EditorStyles.toolbarButton, GUILayout.Width(64f)))
            {
                TagManager.ShowWindow();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(4f);

            if (GUILayout.Button("None", GUILayout.Height(22f)))
            {
                ApplySelection(string.Empty);
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, true, true);
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(contentWidth));

            foreach (var root in roots)
            {
                DrawNode(root, string.Empty, 0, contentWidth);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        private void DrawNode(GameplayTagNode node, string parentPath, int depth, float contentWidth)
        {
            var fullName = GameplayTagEditorUtility.CombinePath(parentPath, node.Segment);
            var hasChildren = node.Children.Count > 0;
            var isExpanded = GetExpanded(fullName, depth == 0);

            var rowRect = EditorGUILayout.GetControlRect(false, RowHeight, GUILayout.Width(contentWidth));
            var indentedRect = new Rect(rowRect.x + depth * IndentWidth, rowRect.y,
                Mathf.Max(0f, rowRect.width - depth * IndentWidth), rowRect.height);
            var foldoutRect = new Rect(indentedRect.x, indentedRect.y + 3f, 18f, indentedRect.height - 6f);
            var bannerRect = new Rect(indentedRect.x + 20f, indentedRect.y, Mathf.Max(140f, indentedRect.width - 22f), indentedRect.height);

            if (hasChildren)
            {
                var nextExpanded = EditorGUI.Foldout(foldoutRect, isExpanded, GUIContent.none, true);
                if (nextExpanded != isExpanded)
                {
                    _expandedStates[fullName] = nextExpanded;
                    isExpanded = nextExpanded;
                }
            }

            EditorGUI.DrawRect(bannerRect, GameplayTagEditorUtility.GetBannerColor(depth));
            if (GUI.Button(bannerRect, fullName, GameplayTagEditorUtility.PopupBannerButtonStyle))
            {
                ApplySelection(fullName);
                return;
            }

            if (!isExpanded)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                DrawNode(child, fullName, depth + 1, contentWidth);
            }
        }

        private bool GetExpanded(string fullName, bool defaultValue)
        {
            if (_expandedStates.TryGetValue(fullName, out var value))
            {
                return value;
            }

            _expandedStates[fullName] = defaultValue;
            return defaultValue;
        }

        private float CalculateContentWidth(IEnumerable<GameplayTagNode> nodes, string parentPath, int depth)
        {
            var maxWidth = 0f;

            foreach (var node in nodes)
            {
                var fullName = GameplayTagEditorUtility.CombinePath(parentPath, node.Segment);
                var labelWidth = GameplayTagEditorUtility.PopupBannerButtonStyle.CalcSize(new GUIContent(fullName)).x;
                var currentWidth = depth * IndentWidth + labelWidth + ExtraContentWidth;
                if (currentWidth > maxWidth)
                {
                    maxWidth = currentWidth;
                }

                var childWidth = CalculateContentWidth(node.Children, fullName, depth + 1);
                if (childWidth > maxWidth)
                {
                    maxWidth = childWidth;
                }
            }

            return maxWidth;
        }

        private void ApplySelection(string tagName)
        {
            var serializedObject = new SerializedObject(_targetObject);
            var property = serializedObject.FindProperty(_propertyPath);
            if (property == null)
            {
                Close();
                return;
            }

            GameplayTagEditorUtility.AssignTag(property, tagName);
            serializedObject.ApplyModifiedProperties();
            Close();
        }
    }
}