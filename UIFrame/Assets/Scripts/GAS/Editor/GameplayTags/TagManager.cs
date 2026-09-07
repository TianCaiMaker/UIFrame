using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GAS.Editor
{
    public class TagManager : EditorWindow
    {
        private const float BannerHeight = 24f;
        private const float IndentWidth = 18f;
        private const float ExtraContentWidth = 132f;

        private GameplayTagDatabase _database;
        private Vector2 _scrollPosition;
        private readonly Dictionary<string, bool> _expandedStates = new Dictionary<string, bool>();
        private string _createParentPath;
        private string _pendingSegment = string.Empty;

        [MenuItem("Tools/GAS/Gameplay Tags")]
        public static void ShowWindow()
        {
            var window = GetWindow<TagManager>("Gameplay Tags");
            window.minSize = new Vector2(420f, 320f);
            window.Show();
        }

        private void OnEnable()
        {
            _database = GameplayTagEditorUtility.GetOrCreateDatabase();
        }

        private void OnGUI()
        {
            _database = GameplayTagEditorUtility.GetOrCreateDatabase();
            var roots = GameplayTagEditorUtility.GetRoots(_database);
            var contentWidth = Mathf.Max(position.width - 32f, CalculateContentWidth(roots, string.Empty, 0));

            DrawToolbar();

            EditorGUILayout.Space(6f);

            if (_database.TagNames.Count == 0)
            {
                EditorGUILayout.HelpBox("No GameplayTag has been created yet. Use the plus button to add the first root tag.",
                    MessageType.Info);
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, true, true);
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(contentWidth));

            for (var index = 0; index < roots.Count; index++)
            {
                DrawNode(roots[index], string.Empty, 0, contentWidth);
            }

            if (_createParentPath == string.Empty)
            {
                DrawCreateRow(string.Empty, 0, contentWidth);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("GameplayTag Manager", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus", "Create root tag"), EditorStyles.toolbarButton,
                    GUILayout.Width(32f)))
            {
                BeginCreate(string.Empty);
            }

            if (GUILayout.Button("Regenerate", EditorStyles.toolbarButton, GUILayout.Width(78f)))
            {
                GameplayTagEditorUtility.SaveDatabase(_database);
                ShowNotification(new GUIContent("GameplayTag code regenerated."));
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawNode(GameplayTagNode node, string parentPath, int depth, float contentWidth)
        {
            var fullName = GameplayTagEditorUtility.CombinePath(parentPath, node.Segment);
            var hasChildren = node.Children.Count > 0;
            var isExpanded = GetExpandedState(fullName, depth == 0);

            var rowRect = EditorGUILayout.GetControlRect(false, BannerHeight, GUILayout.Width(contentWidth));
            var indentedRect = new Rect(rowRect.x + depth * IndentWidth, rowRect.y,
                Mathf.Max(0f, rowRect.width - depth * IndentWidth), rowRect.height);

            var foldoutRect = new Rect(indentedRect.x, indentedRect.y + 3f, 18f, indentedRect.height - 6f);
            var addRect = new Rect(indentedRect.xMax - 56f, indentedRect.y + 2f, 26f, indentedRect.height - 4f);
            var removeRect = new Rect(indentedRect.xMax - 28f, indentedRect.y + 2f, 26f, indentedRect.height - 4f);
            var bannerRect = new Rect(indentedRect.x + 20f, indentedRect.y, Mathf.Max(160f, indentedRect.width - 80f), indentedRect.height);

            if (hasChildren || _createParentPath == fullName)
            {
                var nextExpanded = EditorGUI.Foldout(foldoutRect, isExpanded, GUIContent.none, true);
                if (nextExpanded != isExpanded)
                {
                    _expandedStates[fullName] = nextExpanded;
                    isExpanded = nextExpanded;
                }
            }

            DrawBanner(bannerRect, fullName, depth);

            if (GUI.Button(addRect, EditorGUIUtility.IconContent("Toolbar Plus", "Create child tag"), EditorStyles.iconButton))
            {
                BeginCreate(fullName);
            }

            if (GUI.Button(removeRect, EditorGUIUtility.IconContent("Toolbar Minus", "Delete tag"), EditorStyles.iconButton))
            {
                RemoveTag(fullName);
                return;
            }

            if (!isExpanded)
            {
                return;
            }

            if (_createParentPath == fullName)
            {
                DrawCreateRow(fullName, depth + 1, contentWidth);
            }

            for (var index = 0; index < node.Children.Count; index++)
            {
                DrawNode(node.Children[index], fullName, depth + 1, contentWidth);
            }
        }

        private void DrawBanner(Rect rect, string title, int depth)
        {
            EditorGUI.DrawRect(rect, GameplayTagEditorUtility.GetBannerColor(depth));

            // Right-aligned copy button size and padding
            const float buttonWidth = 64f;
            var buttonHeight = Mathf.Max(14f, rect.height - 8f);
            const float padding = 8f;
            var buttonRect = new Rect(rect.xMax - padding - buttonWidth,
                rect.y + (rect.height - buttonHeight) / 2f, buttonWidth, buttonHeight);

            // Label should avoid overlapping the right button
            var labelLeft = rect.x + 10f;
            var labelRight = buttonRect.x - 6f;
            var labelWidth = Mathf.Max(0f, labelRight - labelLeft);
            var labelRect = new Rect(labelLeft, rect.y + 3f, labelWidth, rect.height - 6f);

            GUI.Label(labelRect, title, GameplayTagEditorUtility.BannerLabelStyle);

            if (GUI.Button(buttonRect, new GUIContent("Copy", "Copy tag path"), EditorStyles.miniButton))
            {
                EditorGUIUtility.systemCopyBuffer = title;
                ShowNotification(new GUIContent($"Copied {title}"));
            }
        }

        private void DrawCreateRow(string parentPath, int depth, float contentWidth)
        {
            var rowRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight + 6f, GUILayout.Width(contentWidth));
            var indentedRect = new Rect(rowRect.x + depth * IndentWidth, rowRect.y,
                Mathf.Max(0f, rowRect.width - depth * IndentWidth), rowRect.height);

            var label = string.IsNullOrEmpty(parentPath) ? "New root tag" : $"New child tag under {parentPath}";
            var labelRect = new Rect(indentedRect.x, indentedRect.y + 2f, 160f, EditorGUIUtility.singleLineHeight);
            var fieldRect = new Rect(indentedRect.x + 162f, indentedRect.y + 2f,
                Mathf.Max(100f, indentedRect.width - 272f), EditorGUIUtility.singleLineHeight);
            var createRect = new Rect(indentedRect.xMax - 104f, indentedRect.y + 1f, 50f, EditorGUIUtility.singleLineHeight + 2f);
            var cancelRect = new Rect(indentedRect.xMax - 52f, indentedRect.y + 1f, 50f, EditorGUIUtility.singleLineHeight + 2f);

            EditorGUI.LabelField(labelRect, label);
            GUI.SetNextControlName("GameplayTagCreateField");
            _pendingSegment = EditorGUI.TextField(fieldRect, _pendingSegment);

            if (GUI.Button(createRect, "Create"))
            {
                CommitCreate(parentPath);
            }

            if (GUI.Button(cancelRect, "Cancel"))
            {
                CancelCreate();
            }

            if (Event.current.type == EventType.Repaint)
            {
                EditorGUI.FocusTextInControl("GameplayTagCreateField");
            }
        }

        private void BeginCreate(string parentPath)
        {
            _createParentPath = parentPath;
            _pendingSegment = string.Empty;

            if (!string.IsNullOrEmpty(parentPath))
            {
                _expandedStates[parentPath] = true;
            }
        }

        private void CommitCreate(string parentPath)
        {
            if (!GameplayTagEditorUtility.TryAddTag(_database, parentPath, _pendingSegment, out var fullName, out var errorMessage))
            {
                EditorUtility.DisplayDialog("GameplayTag", errorMessage, "OK");
                return;
            }

            GameplayTagEditorUtility.SaveDatabase(_database);
            ExpandPath(fullName);
            _createParentPath = null;
            _pendingSegment = string.Empty;
            ShowNotification(new GUIContent($"Created {fullName}"));
        }

        private void CancelCreate()
        {
            _createParentPath = null;
            _pendingSegment = string.Empty;
        }

        private void RemoveTag(string fullName)
        {
            if (!EditorUtility.DisplayDialog("Delete GameplayTag",
                    $"Delete '{fullName}' and all of its child tags?", "Delete", "Cancel"))
            {
                return;
            }

            if (!GameplayTagEditorUtility.TryRemoveTag(_database, fullName))
            {
                EditorUtility.DisplayDialog("GameplayTag", $"Tag '{fullName}' was not found.", "OK");
                return;
            }

            GameplayTagEditorUtility.SaveDatabase(_database);
            _createParentPath = null;
            _pendingSegment = string.Empty;
            ShowNotification(new GUIContent($"Deleted {fullName}"));
        }

        private bool GetExpandedState(string fullName, bool defaultValue)
        {
            if (_expandedStates.TryGetValue(fullName, out var isExpanded))
            {
                return isExpanded;
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
                var labelWidth = GameplayTagEditorUtility.BannerLabelStyle.CalcSize(new GUIContent(fullName)).x;
                var currentWidth = depth * IndentWidth + labelWidth + ExtraContentWidth;
                if (currentWidth > maxWidth)
                {
                    maxWidth = currentWidth;
                }

                if (_createParentPath == fullName)
                {
                    var createLabel = string.IsNullOrEmpty(parentPath)
                        ? "New root tag"
                        : $"New child tag under {fullName}";
                    var createWidth = depth * IndentWidth + EditorStyles.label.CalcSize(new GUIContent(createLabel)).x + 280f;
                    if (createWidth > maxWidth)
                    {
                        maxWidth = createWidth;
                    }
                }

                var childWidth = CalculateContentWidth(node.Children, fullName, depth + 1);
                if (childWidth > maxWidth)
                {
                    maxWidth = childWidth;
                }
            }

            if (_createParentPath == string.Empty)
            {
                var rootCreateWidth = EditorStyles.label.CalcSize(new GUIContent("New root tag")).x + 280f;
                if (rootCreateWidth > maxWidth)
                {
                    maxWidth = rootCreateWidth;
                }
            }

            return maxWidth;
        }

        private void ExpandPath(string fullName)
        {
            var segments = fullName.Split('.');
            var currentPath = string.Empty;

            for (var index = 0; index < segments.Length; index++)
            {
                currentPath = GameplayTagEditorUtility.CombinePath(currentPath, segments[index]);
                _expandedStates[currentPath] = true;
            }
        }
    }
}