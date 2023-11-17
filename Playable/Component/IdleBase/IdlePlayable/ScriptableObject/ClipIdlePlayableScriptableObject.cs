using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    [CreateAssetMenu(fileName = "ClipIdlePlayable", menuName = "ScriptObjects/MiskCore/Playable/IdleBase/Clip")]
    public class ClipIdlePlayableScriptableObject : IdlePlayableScriptableObject
    {
        [SerializeField]
        private AnimationClip _Clip;

        public override IIdlePlayable GetIdle(PlayableGraph graph)
        {
            return new ClipIdlePlayable(graph, _Clip);
        }
    }
}

