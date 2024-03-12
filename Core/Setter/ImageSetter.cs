using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiskCore
{
    [RequireComponent(typeof(Image))]
    public class ImageSetter : MonoBehaviour
    {
        public Image Image
        {
            get
            {
                if (_Image == null)
                {
                    _Image = GetComponent<Image>();
                }

                return _Image;
            }
        }
        private Image _Image;


        public void SetColorR(float value)
        {
            Image.color = new Color(value, Image.color.g, Image.color.b, Image.color.a);
        }

        public void SetColorG(float value)
        {
            Image.color = new Color(Image.color.r, value, Image.color.b, Image.color.a);
        }

        public void SetColorB(float value)
        {
            Image.color = new Color(Image.color.r, Image.color.g, value, Image.color.a);
        }

        public void SetColorA(float value)
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, value);
        }
    }
}

