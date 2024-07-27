using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore.Process
{
    public class ProcessExecuterComponent : MonoBehaviour
    {
        [SerializeField]
        private ProcessComponent m_Process;

        [SerializeField]
        private bool m_ActionOnAwake;

        [SerializeField]
        private bool m_ActionOnNextFrame;

        private void Awake()
        {
            if (m_ActionOnAwake)
                Action();

            if (m_ActionOnNextFrame)
                Utils.NextFrame(Action);
        }

        public void Action()
        {
            TryForceStop();
            StartProcess();
        }
        public void TryForceStop()
        {
            if (!m_Process.IsProcessActive) return;
            m_Process.OnProcessForceStop();
        }

        private void StartProcess()
        {
            if (m_Process.IsProcessActive)
            {
                m_Process.OnProcessForceStop();
            }

            m_Process.Action();
        }
    }
}
