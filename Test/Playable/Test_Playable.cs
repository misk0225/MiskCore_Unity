using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiskCore.Playables.Module.IdleBase
{
    public class Test_Playable : MonoBehaviour
    {
        [SerializeField]
        private IdleBaseComponent _Component;

        [SerializeField]
        private IdlePlayableScriptableObject _IdleSetting;

        [SerializeField]
        private AnimationClip _ActionClip;

        [SerializeField]
        private Slider _ActionSpeedSlider;

        [SerializeField]
        private Slider _IncrementXSlider;

        [SerializeField]
        private Slider _IncrementYSlider;

        [SerializeField]
        private float _MaxIncrementX;

        [SerializeField]
        private float _MaxIncrementY;


        private IIdlePlayable _CurIdle;


        public void Awake()
        {
            _CurIdle = _IdleSetting.GetIdle(_Component.Graph);
            _Component.SetIdlePlayable(_CurIdle);
        }

        public void SetSpeed(float speed)
        {
            _Component.Speed = speed;
        }
        public void SetIdlePlayableSpeed(float speed)
        {
            _Component.IdlePlayable.Speed = speed;
        }
        public void ClipAction()
        {
            _Component.ActionOnceAnimation(new ClipActionOncePlayable(_Component.Graph, _ActionClip, 0.2f, 0.2f), null, _ActionSpeedSlider.value);
        }

        public void SetIncrement()
        {
            if (_CurIdle == null) return;

            if (_CurIdle is IncrementIdlePlayable)
            {
                (_CurIdle as IncrementIdlePlayable).UpdateWeight(_IncrementXSlider.value * _MaxIncrementX);
            }
            else if (_CurIdle is Increment2DIdlePlayable)
            {
                (_CurIdle as Increment2DIdlePlayable).UpdateWeight(_IncrementXSlider.value * _MaxIncrementX, _IncrementYSlider.value * _MaxIncrementY);
            }
        }
    }

}
