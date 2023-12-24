using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiskCore
{
    public class LookAtTransform : MonoBehaviour
    {
        public Transform Target;

        void Awake()
        {
            Utils.Updater(() =>
            {
                transform.LookAt(Target);
            });
        }
    }
}

