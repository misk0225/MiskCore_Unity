using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MiskCore
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPSetter : MonoBehaviour
    {
        private TMP_Text _TMP;

        protected void Awake()
        {
            _TMP = GetComponent<TMP_Text>();
        }

        public void SetColorR(float value)
        {
            _TMP.color = new Color(value, _TMP.color.g, _TMP.color.b, _TMP.color.a);
        }

        public void SetColorG(float value)
        {
            _TMP.color = new Color(_TMP.color.r, value, _TMP.color.b, _TMP.color.a);
        }

        public void SetColorB(float value)
        {
            _TMP.color = new Color(_TMP.color.r, _TMP.color.g, value, _TMP.color.a);
        }

        public void SetColorA(float value)
        {
            _TMP.color = new Color(_TMP.color.r, _TMP.color.g, _TMP.color.b, value);
        }
    }
}
