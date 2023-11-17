using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public abstract class SingletonComponent<T> : MonoBehaviour where T : SingletonComponent<T>
    {
        [SerializeField, Header("(Singleton) Lazy")]
        private bool _Lazy = true;


        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject o = new GameObject(typeof(SingletonComponent<T>).ToString());
                    _Instance = o.AddComponent<T>();

                    if (!_Instance._Lazy)
                    {
                        Debug.LogWarning(typeof(SingletonComponent<T>).ToString() + " �]���D����o�b���椤���͡A�o�i�ಣ�ͥ���lSerialize�ܼư��D�A�нT�{�O�_�ѰO�NComponent�[�i������");
                    }

                    return _Instance;
                }
                else
                {
                    return _Instance;
                }
            }
            private set
            {
                _Instance = value;
            }
        }
        private static T _Instance;

        protected virtual void Awake()
        {
            _Instance = (T) this;
        }
    }
}

