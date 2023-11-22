using System;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UniRx;
using UnityEngine;

namespace MiskCore.Playables.Module.IdleBase
{
    // ���Ѥ@�Ӱʵe����ج[�C
    // �����q�{�ݾ����A�A�åi����@���ʰʵe�A���񧹷|�۰ʤ����^�ݾ��ʵe
    // �H�@�� Mixer �P ClipPlayable ���򩳡A�@���ʪ��ʵe���b Mixer ����
    public class IdleBaseComponent : BasePlayableComponent
    {
        public IIdlePlayable IdlePlayable
        {
            get
            {
                return _IdlePlayable;
            }
        }

        private IIdlePlayable _IdlePlayable;
        private IActionOncePlayable _ActionOncePlayable;
        private AnimationMixerPlayable _RootPlayable;

        private IDisposable _CheckExitConditionDisposable;
        private Action _OnFinish;



        public float Speed 
        {
            get
            {
                return _Speed;
            }

            set
            {
                _Speed = value;
                _RootPlayable.SetSpeed(_Speed);
            }
        }
        private float _Speed = 1f;


        protected override void Awake()
        {
            base.Awake();
            Graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            _RootPlayable = AnimationMixerPlayable.Create(Graph);
            SetIdleToRootPlayable();

            Graph.Play();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _CheckExitConditionDisposable?.Dispose();
        }


        /// <summary>
        /// �]�w Idle �ʵe
        /// </summary>
        public void SetIdlePlayable(IIdlePlayable idlePlayable)
        {
            _IdlePlayable = idlePlayable;
            _IdlePlayable.Playable.SetSpeed(Speed);

            Graph.Disconnect(_RootPlayable, 0);
            Graph.Connect(_IdlePlayable.Playable, 0, _RootPlayable, 0);

            if (_ActionOncePlayable == null)
                _RootPlayable.SetInputWeight(0, 1);
            else
                _RootPlayable.SetInputWeight(0, 1 - _RootPlayable.GetInputWeight(1));
        }

        public void SetIdleToRootPlayable()
        {
            SetRootPlayable(_RootPlayable);
        }


        /// <summary>
        /// ����@�Ӧ欰
        /// �|�۰ʳs�� IActionOncePlayable �� StartMixPlayable
        /// </summary>
        public void ActionOnceAnimation(IActionOncePlayable actionPlayable, Action OnFinish = null, float speed = 1f)
        {
            if (_ActionOncePlayable != null)
            {
                _CheckExitConditionDisposable?.Dispose();
                _ActionOncePlayable.OnExit();
                _OnFinish = null;
            }

            _ActionOncePlayable = actionPlayable;

            Graph.Connect(actionPlayable.Playable, 0, _RootPlayable, 1);
            _ActionOncePlayable.OnStart(this, _RootPlayable, speed);
            _OnFinish = OnFinish;


            StartRefreshAction();
        }

        /// <summary>
        /// �Ȱ����b���񪺦欰
        /// </summary>
        public override void Pause()
        {
            if (Graph.IsPlaying())
            {
                Graph.Stop();
                _ActionOncePlayable?.OnPause();
            }
        }

        /// <summary>
        /// �~�򼽩�欰
        /// </summary>
        public override void Continue()
        {
            if (!Graph.IsPlaying())
            {
                Graph.Play();
                _ActionOncePlayable?.OnContinue();
            }
        }


        private void StartRefreshAction()
        {
            _CheckExitConditionDisposable = Observable.EveryUpdate().Subscribe((_) =>
            {
                if (_ActionOncePlayable.ExitCondition())
                {
                    _CheckExitConditionDisposable.Dispose();
                    _ActionOncePlayable.OnExit();
                    _OnFinish?.Invoke();
                    _ActionOncePlayable = null;
                }
                else
                {
                    _ActionOncePlayable.OnUpdate(Time.deltaTime * _Speed);
                }

            });
        }
    }
}

