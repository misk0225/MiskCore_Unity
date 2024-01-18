using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Process
{
    public class NamableExecutProcessComponent : MonoBehaviour
    {
        private Dictionary<string, ProcessExecuterComponent> m_ExecuterMap = new Dictionary<string, ProcessExecuterComponent>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                var t = transform.GetChild(i);
                m_ExecuterMap.Add(t.name, t.GetComponent<ProcessExecuterComponent>());
            }
        }

        public void Action(string name)
        {
            m_ExecuterMap[name].Action();
        }

        public void ForceStop(string name)
        {
            m_ExecuterMap[name].TryForceStop();
        }
    }
}

