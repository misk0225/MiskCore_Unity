using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace MiskCore
{
    public class MaterialFloatKeySetter : MonoBehaviour
    {
        [SerializeField]
        private Material _Material;

        [SerializeField]
        private string _Key;

        public void SetFloat(float value)
        {
            _Material.SetFloat(_Key, value);
        }
    }
}

