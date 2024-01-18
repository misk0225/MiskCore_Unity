using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace MiskCore
{
    [Serializable]
    public class SequentialEventListeners
    {
        [SerializeField]
        private List<UnityEvent> m_Functions;

        private Action _Action;

        public void Invoke()
        {
            _Action?.Invoke();
            for (int i = 0; i < m_Functions.Count; ++i)
            {
                m_Functions[i].Invoke();
            }
        }


        public static SequentialEventListeners operator +(SequentialEventListeners v1, Action v2)
        {
            v1._Action += v2;
            return v1;
        }
        public static SequentialEventListeners operator -(SequentialEventListeners v1, Action v2)
        {
            v1._Action -= v2;
            return v1;
        }
    }

    [Serializable]
    public class SequentialEventListeners<T1>
    {
        [SerializeField]
        private List<UnityEvent<T1>> m_Functions;

        private Action<T1> _Action;

        public void Invoke(T1 a1)
        {
            _Action?.Invoke(a1);
            for (int i = 0; i < m_Functions.Count; ++i)
            {
                m_Functions[i].Invoke(a1);
            }
        }


        public static SequentialEventListeners<T1> operator +(SequentialEventListeners<T1> v1, Action<T1> v2)
        {
            v1._Action += v2;
            return v1;
        }
        public static SequentialEventListeners<T1> operator -(SequentialEventListeners<T1> v1, Action<T1> v2)
        {
            v1._Action -= v2;
            return v1;
        }
    }


    [Serializable]
    public class SequentialEventListeners<T1, T2>
    {
        [SerializeField]
        private List<UnityEvent<T1, T2>> m_Functions;

        private Action<T1, T2> _Action;

        public void Invoke(T1 a1, T2 a2)
        {
            _Action?.Invoke(a1, a2);
            for (int i = 0; i < m_Functions.Count; ++i)
            {
                m_Functions[i].Invoke(a1, a2);
            }
        }

        public static SequentialEventListeners<T1, T2> operator +(SequentialEventListeners<T1, T2> v1, Action<T1, T2> v2)
        {
            v1._Action += v2;
            return v1;
        }
        public static SequentialEventListeners<T1, T2> operator -(SequentialEventListeners<T1, T2> v1, Action<T1, T2> v2)
        {
            v1._Action -= v2;
            return v1;
        }
    }


    [Serializable]
    public class SequentialEventListeners<T1, T2, T3>
    {
        [SerializeField]
        private List<UnityEvent<T1, T2, T3>> m_Functions;

        private Action<T1, T2, T3> _Action;

        public void Invoke(T1 a1, T2 a2, T3 a3)
        {
            _Action?.Invoke(a1, a2, a3);
            for (int i = 0; i < m_Functions.Count; ++i)
            {
                m_Functions[i].Invoke(a1, a2, a3);
            }
        }

        public static SequentialEventListeners<T1, T2, T3> operator +(SequentialEventListeners<T1, T2, T3> v1, Action<T1, T2, T3> v2)
        {
            v1._Action += v2;
            return v1;
        }
        public static SequentialEventListeners<T1, T2, T3> operator -(SequentialEventListeners<T1, T2, T3> v1, Action<T1, T2, T3> v2)
        {
            v1._Action -= v2;
            return v1;
        }
    }


    [Serializable]
    public class SequentialEventListeners<T1, T2, T3, T4>
    {
        [SerializeField]
        private List<UnityEvent<T1, T2, T3, T4>> m_Functions;

        private Action<T1, T2, T3, T4> _Action;

        public void Invoke(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            _Action?.Invoke(a1, a2, a3, a4);
            for (int i = 0; i < m_Functions.Count; ++i)
            {
                m_Functions[i].Invoke(a1, a2, a3, a4);
            }
        }

        public static SequentialEventListeners<T1, T2, T3, T4> operator +(SequentialEventListeners<T1, T2, T3, T4> v1, Action<T1, T2, T3, T4> v2)
        {
            v1._Action += v2;
            return v1;
        }
        public static SequentialEventListeners<T1, T2, T3, T4> operator -(SequentialEventListeners<T1, T2, T3, T4> v1, Action<T1, T2, T3, T4> v2)
        {
            v1._Action -= v2;
            return v1;
        }
    }
}

