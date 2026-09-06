using System.Collections.Generic;
using UnityEngine;

namespace GAS.Editor
{
    internal class GameplayTagNode
    {
        public GameplayTagNode(string segment)
        {
            Segment = segment;
        }

        public string Segment { get; }

        public List<GameplayTagNode> Children { get; } = new List<GameplayTagNode>();
    }

    internal class GameplayTagDatabase : ScriptableObject
    {
        [SerializeField] private List<string> _tagNames = new List<string>();

        public List<string> TagNames => _tagNames;
    }
}