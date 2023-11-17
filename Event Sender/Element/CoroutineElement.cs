using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MiskCore.EventSender
{
    public class CoroutineElement : BaseEventElement
    {
        private bool isFinish = false;
        private Func<IEnumerator> m_Enumerator;
        private CompositeDisposable disposables;
        private IDisposable m_Timing;

        public CoroutineElement(Func<IEnumerator> enumerator)
        {
            this.m_Enumerator = enumerator;
        }

        protected override void OnStart()
        {
            disposables = new CompositeDisposable();
            m_Timing = Observable.FromCoroutine(m_Enumerator)
            .Subscribe(_ => isFinish = true)
            .AddTo(disposables);
        }

        protected override void OnStop()
        {
            Clear();
        }

        protected override void OnFinish()
        {
            Clear();
        }

        public override bool FinishCondition() => isFinish;


        private void Clear()
        {
            m_Timing.Dispose();
            disposables.Dispose();
        }
    }
}

