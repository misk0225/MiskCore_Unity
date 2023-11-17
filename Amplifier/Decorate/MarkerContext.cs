using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class MarkerContext<T> : DecorateAmplifierValue<T>.Context
    {
        private static readonly string IDENTIFIER = "#MarkerContext";

        private Dictionary<string, Func<T, T>> _FuncMap = new Dictionary<string, Func<T, T>>();

        public void TryAdd(string name, Func<T, T> func)
        {
            string key = name + IDENTIFIER;
            if (!_FuncMap.ContainsKey(name))
            {
                _FuncMap.Add(key, func);
                Amplifier.Add(func);
            }
        }

        public void TryRemove(string name)
        {
            string key = name + IDENTIFIER;
            if (_FuncMap.ContainsKey(name))
            {
                Amplifier.Remove(_FuncMap[key]);
                _FuncMap.Remove(key);
            }
        }
    }

}
