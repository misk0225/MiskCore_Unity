using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiskCore
{
    public class AnimationEventUtility : MonoBehaviour
    {
        [SerializeField]
        private SerializableDictionary<string, SequentialEventListeners> _CallbackMap = new SerializableDictionary<string, SequentialEventListeners>();

        public void Callback(string name)
        {
            _CallbackMap[name].Invoke();
        }

        public void Log(string msg)
        {
            Debug.Log(msg);
        }
    }
}

