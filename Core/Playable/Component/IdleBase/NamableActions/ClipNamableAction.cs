using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// 用AnimationClip做一次性動畫
    /// </summary>
    public class ClipNamableAction : IActionsPlayableNamable
    {
        private Dictionary<string, ClipActionOncePlayable> _Map = new Dictionary<string, ClipActionOncePlayable>();

        private PlayableGraph _Graph;

        public ClipNamableAction(PlayableGraph graph)
        {
            _Graph = graph;
        }

        public void AddClip(string name, AnimationClip clip, float startMix, float exitMix, float speed = 1f)
        {
            _Map.Add(name, new ClipActionOncePlayable(_Graph, clip, startMix, exitMix, speed));
        }

        public IActionOncePlayable GetActionPlayable(string actionName)
        {
            return _Map[actionName];
        }
    }
}

