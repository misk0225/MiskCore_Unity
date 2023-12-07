using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiskCore;


namespace MiskCore 
{
    public class ObjectPool<T> : SingletonComponent<ObjectPool<T>> where T : Component, new()
    {
        public int MaxCount => _MaxCount;
        public int CurCount { get; private set; } = 0;
        public bool HasObject => CurCount < _MaxCount || _Temp.childCount > 0;



        [SerializeField]
        private T _Sample;

        [SerializeField]
        private int _SpawnCountOnAwake = 0;

        [SerializeField]
        private int _MaxCount = 9999999;

        [SerializeField]
        private Transform _Temp;

        [SerializeField]
        private bool _ShowOnGet = true;

        [SerializeField]
        private bool _HideOnRecycle = true;


        protected override void Awake()
        {
            base.Awake();

            if (_Temp == null)
            {
                _Temp = new GameObject("Temp").transform;
            }
            _Temp.parent = this.transform;

            List<T> objs = new List<T>();
            for (int i = 0; i < _SpawnCountOnAwake; ++i)
            {
                T obj = Get();
                if (obj != null)
                {
                    objs.Add(obj);
                }
            }

            foreach (T obj in objs)
            {
                Recycle(obj);
            }
        }

        public T Get()
        {
            if (_Temp.childCount > 0)
            {
                Transform t = _Temp.GetChild(0);
                t.parent = null;
                t.gameObject.SetActive(_ShowOnGet);

                return t.GetComponent<T>();
            }
            else
            {
                if (CurCount < MaxCount)
                {
                    CurCount++;
                    T obj = null;
                    if (_Sample == null)
                        obj = new GameObject(typeof(T).Name).AddComponent<T>();
                    else
                        obj = Instantiate(_Sample);
                    obj.gameObject.SetActive(_ShowOnGet);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }
        public T Get(Vector3 position, Quaternion rotation)
        {
            T obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        public T Get(Vector3 position, Quaternion rotation, Transform parent)
        {
            T obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;
            return obj;
        }
        public T Get(Transform parent)
        {
            T obj = Get();
            obj.transform.parent = parent;
            return obj;
        }

        public void Recycle(T obj)
        {
            obj.gameObject.SetActive(!_HideOnRecycle);
            obj.transform.parent = _Temp;
        }

        public void Clear()
        {
            for (int i = 0; i < _Temp.childCount; ++i)
            {
                Destroy(_Temp.GetChild(i).gameObject);
            }
        }

    }

}
