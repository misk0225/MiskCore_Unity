using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public abstract class BaseFunctionalAnimation : MonoBehaviour
    {
        [SerializeField]
        private float _Time;

        [SerializeField]
        private bool _Loop;

        private IDisposable _Timer;

        protected abstract void OnUpdate(float time, float normalizeTime);

        protected virtual void OnRefresh() { }

        public void Do()
        {
            Clear();

            float cur = 0;
            _Timer = Utils.Updater(() =>
            {
                cur += Time.deltaTime;
                if (_Loop)
                    OnUpdate((cur % _Time), (cur % _Time) / _Time);
                else
                {
                    if (cur >= _Time)
                    {
                        if (_Loop)
                        {
                            cur = cur % _Time;
                            OnRefresh();
                            OnUpdate(cur, cur / _Time);
                        }
                        else
                        {
                            OnUpdate(_Time, 1);
                            Clear();
                        }
                    }
                    else
                    {
                        OnUpdate(cur, cur / _Time);
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
