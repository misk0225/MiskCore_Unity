using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace MiskCore.Playables.Module.IdleBase
{
    [CreateAssetMenu(fileName = "Increment2DIdlePlayable", menuName = "ScriptObjects/MiskCore/Playable/IdleBase/Increment2D")]
    public class Increment2DIdlePlayableObject : IdlePlayableScriptableObject
    {
        [SerializeField]
        private List<IncrementIdlePlayableObject> _Increments;

        [SerializeField]
        private List<float> _Values;

        public override IIdlePlayable GetIdle(PlayableGraph graph)
        {
            return GetIncrement2DIdle(graph);
        }

        public Increment2DIdlePlayable GetIncrement2DIdle(PlayableGraph graph)
        {
            Increment2DIdlePlayable incrementIdlePlayable = new Increment2DIdlePlayable(graph);

            for (int i = 0; i < _Increments.Count; ++i)
            {
                incrementIdlePlayable.AddIncrementPlayable(_Increments[i].GetIncrementIdle(graph).IncrementPlayable, _Values[i]);
            }
            incrementIdlePlayable.UpdateWeight(0, 0);

            return incrementIdlePlayable;
        }
    }
}
