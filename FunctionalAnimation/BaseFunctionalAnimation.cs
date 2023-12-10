using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public abstract class BaseFunctionalAnimation : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f), ReadOnly]
        private float _Process;

        [SerializeField]
        private float _Time;

        [SerializeField]
        private bool _Loop;

        private IDisposable _Timer;

        private void _Update(float time, float normalizeTime)
        {
            _Process = normalizeTime;
            OnUpdate(time, normalizeTime);
        }
        protected abstract void OnUpdate(float time, float normalizeTime);

        protected virtual void OnRefresh() { }

        public void Do()
        {
            Clear();

            OnRefresh();
            _Update(0, 0);

            float cur = 0;
            _Timer = Utils.Updater(() =>
            {
                cur += Time.deltaTime;
                if (cur >= _Time)
                {
                    if (_Loop)
                    {
                        cur = cur % _Time;
                        OnRefresh();
                        _Update(cur, cur / _Time);
                    }
                    else
                    {
                        _Update(_Time, 1);
                        Clear();
                    }
                }
                else
                {
                    _Update(cur, cur / _Time);
                }
            });
        }

        public void Clear()
        {
            _Timer?.Dispose();
        }

        protected void OnDestroy()
        {
            Clear();
        }
    }

}
