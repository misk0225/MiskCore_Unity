using MiskCore.Playables.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    [CreateAssetMenu(fileName = "IncrementIdlePlayable", menuName = "ScriptObjects/MiskCore/Playable/IdleBase/Increment")]
    public class IncrementIdlePlayableObject : IdlePlayableScriptableObject
    {
        [SerializeField]
        private List<AnimationClip> _Clips;

        [SerializeField]
        private List<float> _Values;

        public override IIdlePlayable GetIdle(PlayableGraph graph)
        {
            return GetIncrementIdle(graph);
        }

        public IncrementIdlePlayable GetIncrementIdle(PlayableGraph graph)
        {
            IncrementIdlePlayable incrementIdlePlayable = new IncrementIdlePlayable(graph);

            for (int i = 0; i < _Clips.Count; ++i)
            {
                incrementIdlePlayable.AddPlayable(AnimationClipPlayable.Create(graph, _Clips[i]), _Values[i]);
            }
            incrementIdlePlayable.UpdateWeight(0);

            return incrementIdlePlayable;
        }
    }
}

