using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Process
{
    public class ProcessExecuter
    {
        private static Dictionary<string, ProcessComponent> m_ProcessMap = new Dictionary<string, ProcessComponent>();

        public static void ResigterProcess(ProcessComponent process)
        {
            m_ProcessMap.Add(process.name, process);
        }
        public static void RomoveProcess(string name)
        {
            if (!m_ProcessMap.ContainsKey(name)) return;
            RomoveProcess(m_ProcessMap[name]);
        }

        public static void RomoveProcess(ProcessComponent process)
        {
            if (!m_ProcessMap.ContainsKey(process.name)) return;
            m_ProcessMap.Remove(process.name);
        }

        public static void ForceStopAllProcess()
        {
            foreach (var process in m_ProcessMap.Values)
            {
                TryForceStop(process);
            }
        }

        public static ProcessComponent Action(string name)
        {
            if (!m_ProcessMap.ContainsKey(name)) return null;
            Action(m_ProcessMap[name]);
            return m_ProcessMap[name];
        }

        private static void StartProcess(ProcessComponent process)
        {
            if (process.IsProcessActive)
            {
                process.OnProcessForceStop();
            }

            process.Action();
        }

        public static void Action(ProcessComponent process)
        {
            TryForceStop(process);
            StartProcess(process);
        }

        public static bool TryForceStop(string name)
        {
            if (!m_ProcessMap.ContainsKey(name)) return false;
            return TryForceStop(m_ProcessMap[name]);
        }

        public static bool TryForceStop(ProcessComponent process)
        {
            if (!process.IsProcessActive) return false;

            process.OnProcessForceStop();
            return true;
        }
    }

}
