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

        public void Awake()
        {
            _Component.SetIdlePlayable(_IdleSetting.GetIdle(_Component.Graph));
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
    }

}
