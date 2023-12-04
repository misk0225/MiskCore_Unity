using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MiskCore
{
    public class PropertyAnimation : BaseFunctionalAnimation
    {
        [SerializeField]
        private UnityEvent<float> _Object;

        [SerializeField]
        private AnimationCurve _Curve;

        protected override void OnUpdate(float time, float normalizeTime)
        {
            _Object.Invoke(_Curve.Evaluate(normalizeTime));
        }
    }
}

