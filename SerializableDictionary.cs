using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> _Map;

        public Dictionary<TKey, TValue> _
        {
            get
            {
                if (_Map == null)
                {
                    _Map = new Dictionary<TKey, TValue>();
                    for (int i = 0; i < keys.Count; ++i)
                    {
                        _Map.Add(keys[i], values[i]);
                    }
                }

                return _Map;
            }
        }

        public TValue this [TKey index]
        {
            get
            {
                return _[index];
            }
        }

        public void ContainKey(TKey key) => _.ContainsKey(key);
        public void ContainValue(TValue value) => _.ContainsValue(value);
        public int Count => _.Count;
    }
}
