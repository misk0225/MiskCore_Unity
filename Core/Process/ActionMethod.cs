using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiskCore;

namespace MiskCore.Process
{
    /// <summary>
    /// �i�ۥѱ�����L���ءA�ó]�w Method
    /// �����ߨ�I�sFinish
    /// </summary>
    public class ActionMethod : ProcessComponent
    {
        [SerializeField]
        private SequentialEventListeners _Methods;

        public override void OnProcessStart()
        {
            _Methods.Invoke();
            Finish();
        }
    }

}
