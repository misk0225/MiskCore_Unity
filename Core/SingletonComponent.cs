using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public abstract class SingletonComponent<T> : MonoBehaviour where T : SingletonComponent<T>, new() 
    {
        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject o = new GameObject(typeof(SingletonComponent<T>).ToString());
                    _Instance = o.AddComponent<T>();
                    return _Instance;
                }
                else
                {
                    return _Instance;
                }
            }
            private set
            {
                if (_Instance == null)
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

