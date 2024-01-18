using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    public abstract class NamableActionScriptableObject : ScriptableObject
    {
        public abstract IActionsPlayableNamable GetNamableAction(PlayableGraph graph);
    }
}

