using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class AmplifierValue <T>
    {
        public event Action OnAmplifierChange;

        private Dictionary<Func<T, T>, int> _Amplifiers = new Dictionary<Func<T, T>, int>();

        public int Count(Func<T, T> func)
        {
            if (_Amplifiers.ContainsKey(func))
                return _Amplifiers[func];
            else
                return 0;
        }

        public void Add(Func<T, T> func)
        {
            if (_Amplifiers.ContainsKey(func))
                _Amplifiers[func] = _Amplifiers[func] + 1;
            else
                _Amplifiers[func] = 1;

            OnAmplifierChange?.Invoke();
        }

        public void Remove(Func<T, T> func)
        {
            if (_Amplifiers.ContainsKey(func))
            {
                _Amplifiers[func] = _Amplifiers[func] - 1;
                if (_Amplifiers[func] == 0)
                {
                    _Amplifiers.Remove(func);
                }

                OnAmplifierChange?.Invoke();
            }
        }

        public T Invoke(T value)
        {
            foreach (KeyValuePair<Func<T, T>, int> keyValue in _Amplifiers)
            {
                for (int i = 0; i < keyValue.Value; ++i)
                {
                    value = keyValue.Key(value);
                }
            }

            return value;
        }

        public void ClearAmplifier()
        {
            if (_Amplifiers.Count > 0)
            {
                OnAmplifierChange?.Invoke();
            }

            _Amplifiers.Clear();
        }



        public static AmplifierValue<T> operator + (AmplifierValue<T> v1, Func<T, T> v2)
        {
            v1.Add(v2);
            return v1;
        }
        public static AmplifierValue<T> operator -(AmplifierValue<T> v1, Func<T, T> v2)
        {
            v1.Remove(v2);
            return v1;
        }
    }



    public class AmplifierValue<T, A1>
    {
        public int Count => _Amplifiers.Count;
        public event Action OnAmplifierChange;

        private Dictionary<Func<T, A1, T>, int> _Amplifiers = new Dictionary<Func<T, A1, T>, int>();

        public void Add(Func<T, A1, T> func)
        {
            if (_Amplifiers.ContainsKey(func))
                _Amplifiers[func] = _Amplifiers[func] + 1;
            else
                _Amplifiers[func] = 1;

            OnAmplifierChange?.Invoke();
        }

        public void Remove(Func<T, A1, T> func)
        {
            if (_Amplifiers.ContainsKey(func))
            {
                _Amplifiers[func] = _Amplifiers[func] - 1;
                if (_Amplifiers[func] == 0)
                {
                    _Amplifiers.Remove(func);
                }

                OnAmplifierChange?.Invoke();
            }
        }

        public T Invoke(T value, A1 arg1)
        {
            foreach (KeyValuePair<Func<T, A1, T>, int> keyValue in _Amplifiers)
            {
                for (int i = 0; i < keyValue.Value; ++i)
                {
                    value = keyValue.Key(value, arg1);
                }
            }

            return value;
        }


        public void Clear()
        {
            if (_Amplifiers.Count > 0)
            {
                OnAmplifierChange?.Invoke();
            }

            _Amplifiers.Clear();
        }


        public static AmplifierValue<T, A1> operator +(AmplifierValue<T, A1> v1, Func<T, A1, T> v2)
        {
            v1.Add(v2);
            return v1;
        }
        public static AmplifierValue<T, A1> operator -(AmplifierValue<T, A1> v1, Func<T, A1, T> v2)
        {
            v1.Remove(v2);
            return v1;
        }
    }
}

