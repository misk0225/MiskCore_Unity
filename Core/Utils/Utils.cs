using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class Utils
    {
        public static Vector3 RandomInCircleXZ(Vector3 point, float randis)
        {
            return point + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized * UnityEngine.Random.Range(0, randis);
        }

        public static Vector3 RandomInRingXZ(Vector3 point, float minRandis, float maxRandis)
        {
            return point + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized * UnityEngine.Random.Range(minRandis, maxRandis);
        }


        #region Component

        public static void PlayEffect(GameObjectPool effectPool, Vector3 position, float afterDestory = 3f) => UtilsComponent.Instance.PlayEffect(effectPool, position, afterDestory);
        public static void PlayEffect(ObjectPool<ParticleSystem> effectPool, Vector3 position, float afterDestory = 3f) => UtilsComponent.Instance.PlayEffect(effectPool, position, afterDestory);
        public static void PlayEffect_Retain(ObjectPool<ParticleSystem> effectPool, Vector3 position, float startRetainTime, float afterDestory = 3f) => UtilsComponent.Instance.PlayEffect_Retain(effectPool, position, startRetainTime, afterDestory);
        public static void NextFrame(Action action) => UtilsComponent.Instance.NextFrame(action);
        public static void NextFixFrame(Action action) => UtilsComponent.Instance.NextFixFrame(action);
        public static IDisposable Timer(Action action, float time) => UtilsComponent.Instance.Timer(time, action);
        public static IDisposable Updater(Action action) => UtilsComponent.Instance.Updater(action);
        public static IDisposable Schedule(Action action, float time) => UtilsComponent.Instance.Schedule(time, action);

        #endregion
    }

}
