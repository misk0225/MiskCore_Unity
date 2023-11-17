using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    public abstract class IdlePlayableScriptableObject : ScriptableObject
    {
        public abstract IIdlePlayable GetIdle(PlayableGraph graph);
    }
}
