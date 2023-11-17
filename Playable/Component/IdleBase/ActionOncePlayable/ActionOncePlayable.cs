using System;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    public class ActionOncePlayable : IActionOncePlayable
    {
        public Playable Playable { get; private set; }
        public IdleBaseComponent Component { get; set; }


        private float _Speed = 1f;
        private float _RuntimeSpeed = 1f;
        private float _StartMixScaleTime;
        private float _ExitMixScaleTime;
        private float _StartMixTime;
        private float _ExitMixTime;
        private float _CurTime;
        private float _MaxTime;
        private AnimationMixerPlayable _Mixer;
        private bool _Continue = true;

        private Action<float> _UpdateEvent;

        public ActionOncePlayable(Playable playable, float startMixTime, float exitMixTime, float speed = 1f)
        {
            Playable = playable;
            _StartMixTime = startMixTime;
            _ExitMixTime = exitMixTime;
            _Speed = speed;
        }

        public void OnStart(IdleBaseComponent component, AnimationMixerPlayable mixerPlayable, float speed = 1f)
        {
            _RuntimeSpeed = speed * _Speed;
            _Mixer = mixerPlayable;
            _StartMixScaleTime = _StartMixTime / _RuntimeSpeed;
            _ExitMixScaleTime = _ExitMixTime / _RuntimeSpeed;
            _CurTime = 0;

            Playable.SetTime(0.01f);
            Playable.SetSpeed(_RuntimeSpeed);

            _MaxTime = (float)(Playable.GetDuration() / _RuntimeSpeed);

            StartMix();
        }

        public bool ExitCondition()
        {
            return Playable.GetTime() >= _MaxTime;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_Continue) return;

            _CurTime += deltaTime;
            _UpdateEvent?.Invoke(deltaTime);
        }

        public void OnExit()
        {
            _UpdateEvent = null;
            _Mixer.GetGraph().Disconnect(_Mixer, 1);
            _Mixer.SetInputWeight(0, 1);
            _Mixer.SetInputWeight(1, 0);
        }


        private void StartMix()
        {
            Action<float> update = (t) => { };
            update = (t) => {
                if (_CurTime >= _StartMixScaleTime)
                {
                    _Mixer.SetInputWeight(0, 0);
                    _Mixer.SetInputWeight(1, 1);
                    _UpdateEvent -= update;
                    ExitMix();
                }
                else
                {
                    float weight = _CurTime / _StartMixScaleTime;
                    _Mixer.SetInputWeight(0, 1 - weight);
                    _Mixer.SetInputWeight(1, weight);
                }
            };

            _UpdateEvent += update;
        }

        private void ExitMix()
        {
            Action<float> update = (t) => { };
            update = (t) =>
            {
                if (_CurTime >= _MaxTime)
                {
                    _UpdateEvent -= update;
                }
                else if (_CurTime >= _MaxTime - _ExitMixScaleTime)
                {
                    float weight = 1 - (_MaxTime - _CurTime) / _ExitMixScaleTime;
                    _Mixer.SetInputWeight(0, weight);
                    _Mixer.SetInputWeight(1, 1 - weight);
                }
            };

            _UpdateEvent += update;
        }

        public void OnPause()
        {
            _Continue = false;
        }

        public void OnContinue()
        {
            _Continue = true;
        }
    }
}

