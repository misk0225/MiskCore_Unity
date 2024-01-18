using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore
{
    public class GameObjectPool : SingletonComponent<GameObjectPool>
    {
        public int MaxCount => _MaxCount;
        public int CurCount { get; private set; } = 0;
        public bool HasObject => CurCount < _MaxCount || _Temp.childCount > 0;



        [SerializeField]
        private GameObject _Sample;

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


            List<GameObject> objs = new List<GameObject>();
            for (int i = 0; i < _SpawnCountOnAwake; ++i)
            {
                GameObject obj = Get();
                if (obj != null)
                {
                    objs.Add(obj);
                }
            }

            foreach (GameObject obj in objs)
            {
                Recycle(obj);
            }
        }

        public GameObject Get()
        {
            if (_Temp.childCount > 0)
            {
                Transform t = _Temp.GetChild(0);
                t.parent = null;
                t.gameObject.SetActive(_ShowOnGet);

                return t.gameObject;
            }
            else
            {
                if (CurCount < MaxCount)
                {
                    CurCount++;
                    GameObject obj = Instantiate(_Sample);
                    gameObject.SetActive(_ShowOnGet);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }
        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        public GameObject Get(Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;
            return obj;
        }
        public GameObject Get(Transform parent)
        {
            GameObject obj = Get();
            obj.transform.parent = parent;
            return obj;
        }

        public void Recycle(GameObject obj)
        {
            obj.gameObject.SetActive(!_HideOnRecycle);
            obj.transform.parent = _Temp;
        }

        public void Clear()
        {
            for (int i = 0; i < _Temp.childCount; ++i)
            {
                Destroy(_Temp.GetChild(i));
            }
        }

    }
}
