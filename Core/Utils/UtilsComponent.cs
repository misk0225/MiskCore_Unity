using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MiskCore
{
    public class UtilsComponent : SingletonComponent<UtilsComponent>
    {
        public GameObject PlayEffect(GameObjectPool effectPool, Vector3 position, float afterDestory = 3f)
        {
            GameObject o = effectPool.Get();
            o.transform.position = position;
            Timer(afterDestory, () => effectPool.Recycle(o));

            return o;
        }
        public ParticleSystem PlayEffect(ObjectPool<ParticleSystem> effectPool, Vector3 position, float afterDestory = 3f)
        {
            ParticleSystem o = effectPool.Get();
            o.gameObject.transform.position = position;
            Timer(afterDestory, () => effectPool.Recycle(o));

            return o;
        }

        /// <summary>
        /// 在某處播放一個粒子特效，會先呼叫 ParticleSystem.Stop() 之後再清除
        /// </summary>
        public ParticleSystem PlayEffect_Retain(ObjectPool<ParticleSystem> effectPool, Vector3 position, float startRetainTime, float afterDestory = 3f)
        {
            ParticleSystem o = effectPool.Get();
            o.gameObject.transform.position = position;
            Timer(startRetainTime, () => {
                o.Stop();
                Timer(afterDestory, () => effectPool.Recycle(o));
            });

            return o;
        }

        public void NextFrame(Action action)
        {
            Observable.EveryUpdate().First().Subscribe((_) =>
            {
                action();
            }).AddTo(this);
        }

        public IDisposable Timer(float time, Action action)
        {
            return Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe((_) =>
            {
                action();
            }).AddTo(this);
        }

        public IDisposable Updater(Action action)
        {
            return Observable.EveryUpdate().Subscribe((_) =>
            {
                action();
            }).AddTo(this);
        }

        public IDisposable Schedule(float time, Action action)
        {
            return Observable.Interval(TimeSpan.FromSeconds(time)).Subscribe((_) =>
            {
                action();
            }).AddTo(this);
        }
    }

}
