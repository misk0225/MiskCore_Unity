using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    public class ClipActionOncePlayable : IActionOncePlayable
    {
        public IdleBaseComponent Component { get; set; }
        public Playable Playable { get; private set; }

        private ActionOncePlayable _Action;

        public ClipActionOncePlayable (PlayableGraph graph, AnimationClip clip, float startMixTime = 0, float exitMixTime = 0, float speed = 1f)
        {
            AnimationClipPlayable playable = AnimationClipPlayable.Create(graph, clip);
            playable.SetDuration(clip.length);
            Playable = playable;
            _Action = new ActionOncePlayable(playable, startMixTime, exitMixTime, speed);
        }

        public bool ExitCondition()
        {
            return _Action.ExitCondition();
        }

        public void OnContinue()
        {
            _Action.OnContinue();
        }

        public void OnExit()
        {
            _Action.OnExit();
        }

        public void OnPause()
        {
            _Action.OnPause();
        }

        public void OnStart(IdleBaseComponent component, AnimationMixerPlayable mixerPlayable, float speed)
        {
            _Action.OnStart(component, mixerPlayable, speed);
        }

        public void OnUpdate(float deltaTime)
        {
            _Action.OnUpdate(deltaTime);
        }
    }

}
