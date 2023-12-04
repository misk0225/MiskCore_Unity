using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public class TransformSetter : MonoBehaviour
    {
        public void SetPositionX(float value)
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        public void SetPositionY(float value)
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        public void SetPositionZ(float value)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
        public void SetLocalPositionX(float value)
        {
            transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
        }
        public void SetLocalPositionY(float value)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
        }
        public void SetLocalPositionZ(float value)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
        }


        public void SetRotationX(float value)
        {
            transform.eulerAngles = new Vector3(value, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        public void SetRotationY(float value)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, value, transform.eulerAngles.z);
        }
        public void SetRotationZ(float value)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, value);
        }
        public void SetLocalRotationX(float value)
        {
            transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        public void SetLocalRotationY(float value)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, value, transform.localEulerAngles.z);
        }
        public void SetLocalRotationZ(float value)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, value);
        }


        public void SetScaleX(float value)
        {
            transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        }
        public void SetScaleY(float value)
        {
            transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
        }
        public void SetScaleZ(float value)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
        }
        public void SetLocalScaleX(float value)
        {
            transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        }
        public void SetLocalScaleY(float value)
        {
            transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
        }
        public void SetLocalScaleZ(float value)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
        }
    }
}

