using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore.EventSender
{
    public class ArgInputEventElement : BaseEventElement
    {
        private Action<ArgInputEventElement> onStart = (_) => { };
        private Action<ArgInputEventElement> onUpdate = (_) => { };
        private Action onFinish = () => { };
        private Action onStop = () => { };
        private bool m_IsFinish;

        public ArgInputEventElement(Action<ArgInputEventElement> onStart, Action<ArgInputEventElement> onUpdate = null, Action onFinish = null, Action onStop = null)
        {
            if (onStart != null) this.onStart = onStart;
            if (onUpdate != null) this.onUpdate = onUpdate;
            if (onFinish != null) this.onFinish = onFinish;
            if (onStop != null) this.onStop = onStop;
        }

        protected override void OnStart() 
        {
            m_IsFinish = false;
            onStart(this);
        } 

        protected override void OnUpdate() => onUpdate(this);
        protected override void OnFinish() => onFinish();
        protected override void OnStop() => onStop();

        public void FinishEvent()
        {
            m_IsFinish = true;
        }

        public override bool FinishCondition() => m_IsFinish;

    }
}

