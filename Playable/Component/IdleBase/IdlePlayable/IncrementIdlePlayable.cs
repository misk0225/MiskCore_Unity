using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using MiskCore.Playables.Behaviour;

namespace MiskCore.Playables.Module.IdleBase
{
    public class IncrementIdlePlayable : IIdlePlayable
    {
        public Playable Playable => IncrementPlayable;
        public IncrementBehaviour Behaviour { get; private set; }

        public ScriptPlayable<IncrementBehaviour> IncrementPlayable;

        public float Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
                Playable.SetSpeed(_Speed);
            }
        }

        private float _Speed;

        public IncrementIdlePlayable(PlayableGraph graph)
        {
            IncrementPlayable = ScriptPlayable<IncrementBehaviour>.Create(graph);
            Behaviour = IncrementPlayable.GetBehaviour();
            Behaviour.Init(graph, IncrementPlayable);
            _Speed = 1f;
        }
        public IncrementIdlePlayable(ScriptPlayable<IncrementBehaviour> incrementPlayable)
        {
            IncrementPlayable = incrementPlayable;
            Behaviour = IncrementPlayable.GetBehaviour();
        }

        public void AddPlayable(Playable playable, float minValue)
        {
            Behaviour.AddPlayable(playable, minValue);
        }

        public void UpdateWeight(float weight)
        {
            Behaviour.UpdateWeight(weight);
        }
    }
}

