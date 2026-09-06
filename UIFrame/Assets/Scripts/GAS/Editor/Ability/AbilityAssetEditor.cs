using System;
using System.Collections.Generic;
using GAS.Runtime;
using UnityEditor;
using UnityEngine;

namespace GAS.Editor
{
    [CustomEditor(typeof(AbilityAsset), true)]
    [CanEditMultipleObjects]
    internal sealed class AbilityAssetEditor : UnityEditor.Editor
    {
        private string _lastLoggedConflictMessage;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var uniqueNameProperty = serializedObject.FindProperty(nameof(AbilityAsset.UniqueName));
            var previousUniqueName = uniqueNameProperty?.stringValue;

            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            var inspectorChanged = EditorGUI.EndChangeCheck();

            if (inspectorChanged)
            {
                serializedObject.ApplyModifiedProperties();
            }

            if (targets.Length != 1 || target is not AbilityAsset abilityAsset)
            {
                return;
            }

            var duplicates = AbilityAssetUniqueNameValidator.FindDuplicates(abilityAsset);
            if (duplicates.Count == 0)
            {
                _lastLoggedConflictMessage = null;
                return;
            }

            var message = AbilityAssetUniqueNameValidator.BuildDuplicateMessage(abilityAsset, duplicates);
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(message, MessageType.Error);

            var uniqueNameChanged = inspectorChanged
                                    && uniqueNameProperty != null
                                    && !string.Equals(previousUniqueName, uniqueNameProperty.stringValue,
                                        StringComparison.Ordinal);

            if (uniqueNameChanged && !string.Equals(_lastLoggedConflictMessage, message, StringComparison.Ordinal))
            {
                Debug.LogWarning(message, abilityAsset);
                _lastLoggedConflictMessage = message;
            }
        }
    }

    internal static class AbilityAssetUniqueNameValidator
    {
        public static List<AbilityAsset> FindDuplicates(AbilityAsset target)
        {
            var duplicates = new List<AbilityAsset>();
            var normalizedUniqueName = Normalize(target.UniqueName);
            if (string.IsNullOrEmpty(normalizedUniqueName))
            {
                return duplicates;
            }

            foreach (var asset in EnumerateAbilityAssets())
            {
                if (asset == null || asset == target)
                {
                    continue;
                }

                if (!string.Equals(Normalize(asset.UniqueName), normalizedUniqueName, StringComparison.Ordinal))
                {
                    continue;
                }

                duplicates.Add(asset);
            }

            return duplicates;
        }

        public static string BuildDuplicateMessage(AbilityAsset target, IReadOnlyList<AbilityAsset> duplicates)
        {
            var lines = new List<string>
            {
                $"检测到 AbilityAsset.UniqueName 重复: {target.UniqueName}",
                $"当前文件: {AssetDatabase.GetAssetPath(target)}"
            };

            for (var index = 0; index < duplicates.Count; index++)
            {
                lines.Add($"重复文件 {index + 1}: {AssetDatabase.GetAssetPath(duplicates[index])}");
            }

            return string.Join("\n", lines);
        }

        private static IEnumerable<AbilityAsset> EnumerateAbilityAssets()
        {
            var seenGuids = new HashSet<string>();

            foreach (var type in TypeCache.GetTypesDerivedFrom<AbilityAsset>())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                foreach (var guid in AssetDatabase.FindAssets($"t:{type.Name}"))
                {
                    if (!seenGuids.Add(guid))
                    {
                        continue;
                    }

                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<AbilityAsset>(path);
                    if (asset != null)
                    {
                        yield return asset;
                    }
                }
            }
        }

        private static string Normalize(string value)
        {
            return value?.Trim();
        }
    }
}