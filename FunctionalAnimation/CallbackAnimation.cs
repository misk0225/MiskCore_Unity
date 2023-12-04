using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiskCore
{
    public class CallbackAnimation : BaseFunctionalAnimation
    {
        [SerializeField, Header("Callback Ĳ�o�ɶ��]�бN�ɶ��Ѥp��j�Ƨǡ^")]
        private List<Fragment> _Fragments;

        [SerializeField, Header("Callback Ĳ�o�ɶ��]�k�@�ơ^�]�бN�ɶ��Ѥp��j�Ƨǡ^")]
        private List<Fragment> _NormalizeFragments;

        private int _Idx = 0;
        private int _NormalizeIdx = 0;

        protected override void OnUpdate(float time, float normalizeTime)
        {
            while (_Idx < _Fragments.Count)
            {
                if (_Fragments[_Idx].Time <= time)
                    _Fragments[_Idx++].Callback.Invoke();
                else
                    break;
            }

            while (_NormalizeIdx < _NormalizeFragments.Count)
            {
                if (_NormalizeFragments[_NormalizeIdx].Time <= normalizeTime)
                    _NormalizeFragments[_NormalizeIdx++].Callback.Invoke();
                else
                    break;
            }
        }

        protected override void OnRefresh()
        {
            _Idx = 0;

        }


        [Serializable]
        private struct Fragment
        {
            public float Time;
            public SequentialEventListeners Callback;
        }
    }
}

