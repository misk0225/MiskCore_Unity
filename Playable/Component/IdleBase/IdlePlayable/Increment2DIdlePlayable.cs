using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using MiskCore.Playables.Behaviour;


namespace MiskCore.Playables.Module.IdleBase
{
    public class Increment2DIdlePlayable : IIdlePlayable
    {
        public Playable Playable => IncrementPlayable2D;

        public IncrementBehaviour Behaviour2D { get; private set; }

        public ScriptPlayable<IncrementBehaviour> IncrementPlayable2D;
        public List<IncrementBehaviour> IncrementBehaviour1Ds = new List<IncrementBehaviour>();

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


        public Increment2DIdlePlayable(PlayableGraph graph)
        {
            IncrementPlayable2D = ScriptPlayable<IncrementBehaviour>.Create(graph);
            Behaviour2D = IncrementPlayable2D.GetBehaviour();
            Behaviour2D.Init(graph, IncrementPlayable2D);
            _Speed = 1f;
        }
        public Increment2DIdlePlayable(ScriptPlayable<IncrementBehaviour> incrementPlayable)
        {
            IncrementPlayable2D = incrementPlayable;
            Behaviour2D = IncrementPlayable2D.GetBehaviour();
        }

        public void AddIncrementPlayable(ScriptPlayable<IncrementBehaviour> playable, float minvalue)
        {
            IncrementBehaviour1Ds.Add(playable.GetBehaviour());
            Behaviour2D.AddPlayable(playable, minvalue);
        }

        public void UpdateWeight(float weight1D, float weight2D)
        {
            UpdateWeight1D(weight1D);
            UpdateWeight2D(weight2D);
        }

        public void UpdateWeight1D(float weight) 
        {
            foreach (var behaviour in IncrementBehaviour1Ds)
                behaviour.UpdateWeight(weight);
        }

        public void UpdateWeight2D(float weight)
        {
            Behaviour2D.UpdateWeight(weight);
        }
    }
}

