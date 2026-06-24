using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tools
{
    public class StringId : Singleton<StringId>
    {
        private readonly Dictionary<string, int> _stringToId = new Dictionary<string, int>();
        private readonly Dictionary<int, string> _idToString = new Dictionary<int, string>();
        private readonly object _lock = new object();

        public int GetId(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            lock (_lock)
            {
                if (_stringToId.TryGetValue(str, out int existingId))
                    return existingId;

                int id = str.GetHashCode();
                // linear probe until an unused id or the same string is found
                while (_idToString.TryGetValue(id, out string mapped) && mapped != str)
                {
                    id++;
                }

                // store mappings
                _stringToId[str] = id;
                _idToString[id] = str;
                return id;
            }
        }
    }
}