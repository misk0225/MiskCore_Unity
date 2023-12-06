using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiskCore
{
    [RequireComponent(typeof(Image))]
    public class ImageSetter : MonoBehaviour
    {
        private Image _Image;

        protected void Awake()
        {
            _Image = GetComponent<Image>();
        }

        public void SetColorR(float value)
        {
            _Image.color = new Color(value, _Image.color.g, _Image.color.b, _Image.color.a);
        }

        public void SetColorG(float value)
        {
            _Image.color = new Color(_Image.color.r, value, _Image.color.b, _Image.color.a);
        }

        public void SetColorB(float value)
        {
            _Image.color = new Color(_Image.color.r, _Image.color.g, value, _Image.color.a);
        }

        public void SetColorA(float value)
        {
            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, value);
        }
    }
}

