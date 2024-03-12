using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MiskCore
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMPSetter : MonoBehaviour
    {
        public TextMeshProUGUI TMP
        {
            get
            {
                if (_TMP == null)
                {
                    _TMP = GetComponent<TextMeshProUGUI>();
                }

                return _TMP;
            }
        }
        private TextMeshProUGUI _TMP;

        public void SetColorR(float value)
        {
            TMP.color = new Color(value, TMP.color.g, TMP.color.b, TMP.color.a);
        }

        public void SetColorG(float value)
        {
            TMP.color = new Color(TMP.color.r, value, TMP.color.b, TMP.color.a);
        }

        public void SetColorB(float value)
        {
            TMP.color = new Color(TMP.color.r, TMP.color.g, value, TMP.color.a);
        }

        public void SetColorA(float value)
        {
            TMP.color = new Color(TMP.color.r, TMP.color.g, TMP.color.b, value);
        }
    }
}
