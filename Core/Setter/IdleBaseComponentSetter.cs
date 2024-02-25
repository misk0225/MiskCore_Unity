using MiskCore.Playables.Module.IdleBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore
{
    [RequireComponent(typeof(IdleBaseComponent))]
    public class IdleBaseComponentSetter : MonoBehaviour
    {
        private IdleBaseComponent _Component;

        private IncrementIdlePlayable _incrementIdlePlayable;
        private Increment2DIdlePlayable _incrementIdle2DPlayable;

        void Awake()
        {
            _Component = GetComponent<IdleBaseComponent>();
        }

        public void SetIncrementIdlePlayableWeight(float weight)
        {
            if (TryGetIncrementIdlePlayable(ref _incrementIdlePlayable))
            {
                _incrementIdlePlayable.UpdateWeight(weight);
            }
        }

        public void SetIncrementIdlePlayableWeight1D(float weight)
        {
            if (TryGetIncrementIdlePlayable(ref _incrementIdle2DPlayable))
            {
                _incrementIdle2DPlayable.UpdateWeight1D(weight);
            }
        }

        public void SetIncrementIdlePlayableWeight2D(float weight)
        {
            if (TryGetIncrementIdlePlayable(ref _incrementIdle2DPlayable))
            {
                _incrementIdle2DPlayable.UpdateWeight2D(weight);
            }
        }


        private bool TryGetIncrementIdlePlayable<T>(ref T Idleplayable) where T : class, IIdlePlayable
        {
            if (Idleplayable == null)
            {
                Idleplayable = _Component.IdlePlayable as T;
            }
            else
            {
                if (Idleplayable != _Component.IdlePlayable)
                {
                    Idleplayable = _Component.IdlePlayable as T;
                }
            }

            return Idleplayable != null;
        }
    }
}
