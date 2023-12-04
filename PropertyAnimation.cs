using MiskCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MiskCore
{
    public class PropertyAnimation : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<float> _Object;

        [SerializeField]
        private AnimationCurve _Curve;

        [SerializeField]
        private float _Time;

        [SerializeField]
        private bool _Loop;

        private IDisposable _Timer;

        public void Do()
        {
            Clear();

            float cur = 0;
            _Timer = Utils.Updater(() =>
            {
                cur += Time.deltaTime;
                if (_Loop)
                    _Object.Invoke(_Curve.Evaluate((cur % _Time) / _Time));
                else
                {
                    if (cur >= _Time)
                    {
                        _Object.Invoke(_Curve.Evaluate(1));
                        Clear();
                    }
                    else
                    {
                        _Object.Invoke(_Curve.Evaluate(cur / _Time));
                    }
                }
            });
        }

        public void Clear()
        {
            _Timer?.Dispose();
        }
    }
}

