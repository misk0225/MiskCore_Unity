using System;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UniRx;
using UnityEngine;

namespace MiskCore.Playables.Module.IdleBase
{
    // 提供一個動畫控制器框架。
    // 播放默認待機狀態，並可播放一次性動畫，播放完會自動切換回待機動畫
    // 以一個 Mixer 與 ClipPlayable 為基底，一次性的動畫接在 Mixer 分支
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
        /// 設定 Idle 動畫
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
        /// 執行一個行為
        /// 會自動連接 IActionOncePlayable 的 StartMixPlayable
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
        /// 暫停正在播放的行為
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
        /// 繼續播放行為
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

