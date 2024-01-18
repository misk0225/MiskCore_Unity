using MiskCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore
{
    public class TimerContext<T> : DecorateAmplifierValue<T>.Context
    {
        private static readonly string IDENTIFIER = "#TimerContext";

        private Dictionary<string, TimerInfo> _Map = new Dictionary<string, TimerInfo>();

        public void Set(string name, Func<T, T> func, float time)
        {
            TryRemove(name);

            string key = name + IDENTIFIER;

            Amplifier.Add(func);
            TimerInfo info = new TimerInfo {
                func = func,
                disposable = Utils.Schedule(() =>
                {
                    TryRemove(name);
                }, time)
            };

            _Map.Add(key, info);
        }


        public void TryRemove(string name)
        {
            string key = name + IDENTIFIER;
            if (_Map.ContainsKey(key))
            {
                _Map[key].disposable?.Dispose();
                Amplifier.Remove(_Map[key].func);
                _Map.Remove(key);
            }
        }

        private struct TimerInfo
        {
            public Func<T, T> func;
            public IDisposable disposable;
        }
    }
}
