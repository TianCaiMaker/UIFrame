using System.Collections.Generic;

namespace General
{
    public class GeneralContext
    {
        private Dictionary<string, object> _objects = new Dictionary<string, object>();
        private Dictionary<string, bool> _booleans = new Dictionary<string, bool>();
        private Dictionary<string, int> _integers = new Dictionary<string, int>();
        private Dictionary<string, float> _floats = new Dictionary<string, float>();
        private Dictionary<string, double> _doubles = new Dictionary<string, double>();
        private Dictionary<string, string> _strings = new Dictionary<string, string>();
        private Dictionary<string, long> _longs = new Dictionary<string, long>();
        public void SetBoolean(string key, bool value)
        {
            if (_booleans.ContainsKey(key))
            {
                _booleans[key] = value;
            }
            else
            {
                _booleans.Add(key, value);
            }
        }
        public bool GetBoolean(string key)
        {
            if (_booleans.ContainsKey(key))
            {
                return _booleans[key];
            }
            return false;
        }
        public void SetInteger(string key, int value)
        {
            if (_integers.ContainsKey(key))
            {
                _integers[key] = value;
            }
            else
            {
                _integers.Add(key, value);
            }
        }
        public int GetInteger(string key)
        {
            if (_integers.ContainsKey(key))
            {
                return _integers[key];
            }
            return 0;
        }
        public void SetFloat(string key, float value)
        {
            if (_floats.ContainsKey(key))
            {
                _floats[key] = value;
            }
            else
            {
                _floats.Add(key, value);
            }
        }
        public float GetFloat(string key)
        {
            if (_floats.ContainsKey(key))
            {
                return _floats[key];
            }
            return 0f;
        }
        public void SetDouble(string key, double value)
        {
            if (_doubles.ContainsKey(key))
            {
                _doubles[key] = value;
            }
            else
            {
                _doubles.Add(key, value);
            }
        }
        public double GetDouble(string key)
        {
            if (_doubles.ContainsKey(key))
            {
                return _doubles[key];
            }
            return 0.0;
        }
        public void SetString(string key, string value)
        {
            if (_strings.ContainsKey(key))
            {
                _strings[key] = value;
            }
            else
            {
                _strings.Add(key, value);
            }
        }
        public string GetString(string key)
        {
            if (_strings.ContainsKey(key))
            {
                return _strings[key];
            }
            return string.Empty;
        }
        public void SetLong(string key, long value)
        {
            if (_longs.ContainsKey(key))
            {
                _longs[key] = value;
            }
            else
            {
                _longs.Add(key, value);
            }
        }
        public long GetLong(string key)
        {
            if (_longs.ContainsKey(key))
            {
                return _longs[key];
            }
            return 0L;
        }
        public void Set<T>(string key, T value)
        {
            if (_objects.ContainsKey(key))
            {
                _objects[key] = value;
            }
            else
            {
                _objects.Add(key, value);
            }
        }
        public T Get<T>(string key)
        {
            if (_objects.ContainsKey(key))
            {
                if (_objects[key] is T value)
                {
                    return value;
                }
                else
                {
                    return default(T);
                }
            }
            return default(T);
        }
    }
}
