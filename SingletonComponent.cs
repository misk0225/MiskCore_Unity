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
                        Debug.LogWarning(typeof(SingletonComponent<T>).ToString() + " 設為非延遲卻在執行中產生，這可能產生未初始Serialize變數問題，請確認是否忘記將Component加進場景中");
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

