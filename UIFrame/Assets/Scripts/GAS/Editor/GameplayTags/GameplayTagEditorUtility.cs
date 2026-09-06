using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GAS.Runtime;
using UnityEditor;
using UnityEngine;

namespace GAS.Editor
{
    internal static class GameplayTagEditorUtility
    {
        private const string DefaultDatabaseFolderPath = "Assets/GAS/Editor/GameplayTags/EditorFile";
        private const string LegacyDatabaseAssetPath = "Assets/GAS/Editor/EditorFile/GameplayTagDatabase.asset";
        private const string DatabaseAssetFileName = "GameplayTagDatabase.asset";
        private const string GeneratedScriptPath = "Assets/GAS/Runtime/Tags/GTagLib.gen.cs";
        private const string EditorDataFolderName = "EditorFile";

        private static GUIStyle _bannerLabelStyle;
        private static GUIStyle _popupBannerButtonStyle;
        private static string _databaseFolderPath;

        public static GUIStyle BannerLabelStyle
        {
            get
            {
                if (_bannerLabelStyle != null)
                {
                    return _bannerLabelStyle;
                }

                _bannerLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleLeft,
                    padding = new RectOffset(0, 0, 0, 0),
                    normal = { textColor = Color.white }
                };
                return _bannerLabelStyle;
            }
        }

        public static GUIStyle PopupBannerButtonStyle
        {
            get
            {
                if (_popupBannerButtonStyle != null)
                {
                    return _popupBannerButtonStyle;
                }

                _popupBannerButtonStyle = new GUIStyle(EditorStyles.miniButton)
                {
                    alignment = TextAnchor.MiddleLeft,
                    fontStyle = FontStyle.Bold,
                    normal = { textColor = Color.white },
                    hover = { textColor = Color.white },
                    active = { textColor = Color.white }
                };
                return _popupBannerButtonStyle;
            }
        }

        public static GameplayTagDatabase GetOrCreateDatabase()
        {
            var databaseFolderPath = GetDatabaseFolderPath();
            EnsureFolder(databaseFolderPath);

            var databaseAssetPath = CombineAssetPath(databaseFolderPath, DatabaseAssetFileName);
            MigrateLegacyAssetIfNeeded(databaseAssetPath);

            var database = AssetDatabase.LoadAssetAtPath<GameplayTagDatabase>(databaseAssetPath);
            if (database == null)
            {
                database = ScriptableObject.CreateInstance<GameplayTagDatabase>();
                AssetDatabase.CreateAsset(database, databaseAssetPath);
                AssetDatabase.SaveAssets();
            }

            TryMigrateLegacyTreeData(database, databaseAssetPath);
            GenerateRuntimeLibrary(database);
            return database;
        }

        public static void SaveDatabase(GameplayTagDatabase database)
        {
            SortAndDeduplicateTagNames(database.TagNames);
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            GenerateRuntimeLibrary(database);
            AssetDatabase.Refresh();
        }

        public static List<GameplayTagNode> GetRoots(GameplayTagDatabase database)
        {
            var roots = new List<GameplayTagNode>();
            var rootMap = new Dictionary<string, GameplayTagNode>(StringComparer.OrdinalIgnoreCase);

            foreach (var tagName in database.TagNames.OrderBy(value => value, StringComparer.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(tagName))
                {
                    continue;
                }

                var segments = tagName.Split('.');
                var currentChildren = roots;
                var currentMap = rootMap;
                var currentPath = string.Empty;

                for (var index = 0; index < segments.Length; index++)
                {
                    var segment = segments[index];
                    currentPath = CombinePath(currentPath, segment);

                    if (!currentMap.TryGetValue(currentPath, out var node))
                    {
                        node = new GameplayTagNode(segment);
                        currentChildren.Add(node);
                        currentMap[currentPath] = node;
                    }

                    currentChildren = node.Children;
                }
            }

            return roots;
        }

        public static bool TryAddTag(GameplayTagDatabase database, string parentPath, string segment, out string fullName,
            out string errorMessage)
        {
            fullName = string.Empty;
            errorMessage = string.Empty;

            segment = segment == null ? string.Empty : segment.Trim();
            if (!TryValidateSegment(segment, out errorMessage))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(parentPath) && !ContainsTag(database.TagNames, parentPath))
            {
                errorMessage = $"Parent tag '{parentPath}' was not found.";
                return false;
            }

            fullName = CombinePath(parentPath, segment);

            if (ContainsTag(database.TagNames, fullName))
            {
                errorMessage = $"GameplayTag '{fullName}' already exists.";
                return false;
            }

            database.TagNames.Add(fullName);
            return true;
        }

        public static bool TryRemoveTag(GameplayTagDatabase database, string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return false;
            }

            var removedCount = database.TagNames.RemoveAll(tagName =>
                string.Equals(tagName, fullName, StringComparison.OrdinalIgnoreCase) ||
                tagName.StartsWith(fullName + ".", StringComparison.OrdinalIgnoreCase));

            return removedCount > 0;
        }

        public static void AssignTag(SerializedProperty property, string tagName)
        {
            var nameProperty = property.FindPropertyRelative("_name");
            var hashCodeProperty = property.FindPropertyRelative("_hashCode");
            var shortNameProperty = property.FindPropertyRelative("_shortName");
            var ancestorHashCodesProperty = property.FindPropertyRelative("_ancestorHashCodes");
            var ancestorNamesProperty = property.FindPropertyRelative("_ancestorNames");

            if (string.IsNullOrEmpty(tagName))
            {
                nameProperty.stringValue = string.Empty;
                hashCodeProperty.intValue = 0;
                shortNameProperty.stringValue = string.Empty;
                ancestorHashCodesProperty.arraySize = 0;
                ancestorNamesProperty.arraySize = 0;
                return;
            }

            var gameplayTag = new GameplayTag(tagName);
            nameProperty.stringValue = gameplayTag.Name;
            hashCodeProperty.intValue = gameplayTag.HashCode;
            shortNameProperty.stringValue = gameplayTag.ShortName;

            ancestorHashCodesProperty.arraySize = gameplayTag.AncestorHashCodes.Length;
            for (var index = 0; index < gameplayTag.AncestorHashCodes.Length; index++)
            {
                ancestorHashCodesProperty.GetArrayElementAtIndex(index).intValue = gameplayTag.AncestorHashCodes[index];
            }

            ancestorNamesProperty.arraySize = gameplayTag.AncestorNames.Length;
            for (var index = 0; index < gameplayTag.AncestorNames.Length; index++)
            {
                ancestorNamesProperty.GetArrayElementAtIndex(index).stringValue = gameplayTag.AncestorNames[index];
            }
        }

        public static string CombinePath(string parentPath, string segment)
        {
            if (string.IsNullOrEmpty(parentPath))
            {
                return segment;
            }

            return string.IsNullOrEmpty(segment) ? parentPath : $"{parentPath}.{segment}";
        }

        public static Color GetBannerColor(int depth)
        {
            switch (depth % 4)
            {
                case 0:
                    return new Color(0.21f, 0.42f, 0.58f, 1f);
                case 1:
                    return new Color(0.18f, 0.52f, 0.44f, 1f);
                case 2:
                    return new Color(0.58f, 0.38f, 0.18f, 1f);
                default:
                    return new Color(0.43f, 0.29f, 0.56f, 1f);
            }
        }

        private static string GetDatabaseFolderPath()
        {
            if (!string.IsNullOrEmpty(_databaseFolderPath))
            {
                return _databaseFolderPath;
            }

            var scriptGuids = AssetDatabase.FindAssets($"{nameof(GameplayTagEditorUtility)} t:Script");
            foreach (var guid in scriptGuids)
            {
                var scriptPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileNameWithoutExtension(scriptPath) != nameof(GameplayTagEditorUtility))
                {
                    continue;
                }

                var directoryPath = Path.GetDirectoryName(scriptPath);
                if (string.IsNullOrEmpty(directoryPath))
                {
                    continue;
                }

                _databaseFolderPath = CombineAssetPath(directoryPath.Replace('\\', '/'), EditorDataFolderName);
                return _databaseFolderPath;
            }

            _databaseFolderPath = DefaultDatabaseFolderPath;
            return _databaseFolderPath;
        }

        private static void EnsureFolder(string targetFolderPath)
        {
            if (AssetDatabase.IsValidFolder(targetFolderPath))
            {
                return;
            }

            var parts = targetFolderPath.Split('/');
            if (parts.Length == 0)
            {
                return;
            }

            var currentPath = parts[0];
            for (var index = 1; index < parts.Length; index++)
            {
                var nextPath = CombineAssetPath(currentPath, parts[index]);
                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    AssetDatabase.CreateFolder(currentPath, parts[index]);
                }

                currentPath = nextPath;
            }
        }

        private static void MigrateLegacyAssetIfNeeded(string databaseAssetPath)
        {
            if (databaseAssetPath == LegacyDatabaseAssetPath)
            {
                return;
            }

            if (AssetDatabase.LoadAssetAtPath<GameplayTagDatabase>(databaseAssetPath) != null)
            {
                return;
            }

            if (AssetDatabase.LoadAssetAtPath<GameplayTagDatabase>(LegacyDatabaseAssetPath) == null)
            {
                return;
            }

            var moveResult = AssetDatabase.MoveAsset(LegacyDatabaseAssetPath, databaseAssetPath);
            if (!string.IsNullOrEmpty(moveResult))
            {
                Debug.LogWarning($"Failed to move GameplayTagDatabase asset to the new EditorFile folder: {moveResult}");
            }
        }

        private static bool TryValidateSegment(string segment, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                errorMessage = "GameplayTag name cannot be empty.";
                return false;
            }

            if (segment.Contains('.'))
            {
                errorMessage = "GameplayTag name cannot contain '.'. Use child tags instead.";
                return false;
            }

            if (segment.Any(char.IsWhiteSpace))
            {
                errorMessage = "GameplayTag name cannot contain whitespace.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private static bool ContainsTag(List<string> tagNames, string fullName)
        {
            return tagNames.Any(tagName => string.Equals(tagName, fullName, StringComparison.OrdinalIgnoreCase));
        }

        private static void SortAndDeduplicateTagNames(List<string> tagNames)
        {
            for (var index = tagNames.Count - 1; index >= 0; index--)
            {
                if (string.IsNullOrWhiteSpace(tagNames[index]))
                {
                    tagNames.RemoveAt(index);
                }
            }

            var ordered = tagNames
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList();

            tagNames.Clear();
            tagNames.AddRange(ordered);
        }

        private static void GenerateRuntimeLibrary(GameplayTagDatabase database)
        {
            SortAndDeduplicateTagNames(database.TagNames);

            var builder = new StringBuilder();
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine();
            builder.AppendLine("namespace GAS.Runtime");
            builder.AppendLine("{");
            builder.AppendLine("    public static class GTagLib");
            builder.AppendLine("    {");
            builder.AppendLine("        public static readonly Dictionary<string, GameplayTag> TagMap =");
            builder.AppendLine("            new Dictionary<string, GameplayTag>");
            builder.AppendLine("            {");

            foreach (var tag in database.TagNames)
            {
                builder.AppendLine($"                {{ \"{tag}\", new GameplayTag(\"{tag}\") }},");
            }

            builder.AppendLine("            };");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            var projectRoot = Directory.GetParent(Application.dataPath);
            if (projectRoot == null)
            {
                return;
            }

            var generatedFilePath = Path.Combine(projectRoot.FullName, GeneratedScriptPath.Replace('/', Path.DirectorySeparatorChar));
            var newContent = builder.ToString();

            if (File.Exists(generatedFilePath))
            {
                var currentContent = File.ReadAllText(generatedFilePath);
                if (string.Equals(currentContent, newContent, StringComparison.Ordinal))
                {
                    return;
                }
            }

            File.WriteAllText(generatedFilePath, newContent, Encoding.UTF8);
        }

        private static void TryMigrateLegacyTreeData(GameplayTagDatabase database, string databaseAssetPath)
        {
            if (database.TagNames.Count > 0)
            {
                return;
            }

            var projectRoot = Directory.GetParent(Application.dataPath);
            if (projectRoot == null)
            {
                return;
            }

            var fullDatabasePath = Path.Combine(projectRoot.FullName, databaseAssetPath.Replace('/', Path.DirectorySeparatorChar));
            if (!File.Exists(fullDatabasePath))
            {
                return;
            }

            var tagNames = ParseLegacyTagNames(File.ReadAllLines(fullDatabasePath));
            if (tagNames.Count == 0)
            {
                return;
            }

            database.TagNames.AddRange(tagNames);
            SortAndDeduplicateTagNames(database.TagNames);
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static List<string> ParseLegacyTagNames(IEnumerable<string> lines)
        {
            var tagNames = new List<string>();
            var segments = new List<string>();

            foreach (var rawLine in lines)
            {
                var line = rawLine.TrimEnd();
                var markerIndex = line.IndexOf("- _segment:", StringComparison.Ordinal);
                if (markerIndex < 0)
                {
                    continue;
                }

                var indent = rawLine.TakeWhile(char.IsWhiteSpace).Count();
                var depth = Math.Max(0, indent / 2 - 1);
                var segment = line.Substring(markerIndex + "- _segment:".Length).Trim();
                if (string.IsNullOrEmpty(segment))
                {
                    continue;
                }

                while (segments.Count > depth)
                {
                    segments.RemoveAt(segments.Count - 1);
                }

                if (segments.Count == depth)
                {
                    segments.Add(segment);
                }
                else
                {
                    segments[depth] = segment;
                }

                tagNames.Add(string.Join(".", segments));
            }

            return tagNames;
        }

        private static string CombineAssetPath(string left, string right)
        {
            if (string.IsNullOrEmpty(left))
            {
                return right;
            }

            if (string.IsNullOrEmpty(right))
            {
                return left;
            }

            return $"{left.TrimEnd('/')}/{right.TrimStart('/')}";
        }
    }
}