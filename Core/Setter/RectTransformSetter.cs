using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class RectTransformSetter : TransformSetter
    {
        private RectTransform _RectTransform;

        protected void Awake()
        {
            _RectTransform = GetComponent<RectTransform>();
        }

        public void SetWidth(float value)
        {
            _RectTransform.sizeDelta = new Vector2(value, _RectTransform.sizeDelta.y);
        }

        public void SetHeight(float value)
        {
            _RectTransform.sizeDelta = new Vector2(_RectTransform.sizeDelta.x, value);
        }

        public void SetAnchoredPositionX(float value)
        {
            _RectTransform.anchoredPosition = new Vector2(value, _RectTransform.anchoredPosition.y);
        }

        public void SetAnchoredPositionY(float value)
        {
            _RectTransform.anchoredPosition = new Vector2(_RectTransform.anchoredPosition.x, value);
        }
    }

}
