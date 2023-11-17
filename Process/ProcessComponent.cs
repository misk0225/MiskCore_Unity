using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Process
{
    /// <summary>
    /// Process Component �O�@�Ӽs�x���A���A�o�W�ߪ��y�{����A�b����� GameObject �|�G�_�A�]�N�O���֦��M���������i�ݥX���b���檺�y�{
    /// �H State Machine �Ҧ�����{���A����O���󦡡B�i�ǦC�ơB�i��ܪ� State Machine
    /// </summary>
    public abstract class ProcessComponent : MonoBehaviour
    {
        public Action onStart;
        public Action onFinish;

        public bool IsProcessActive
        {
            get
            {
                return gameObject.activeSelf;
            }
        }

        public void Action()
        {
            gameObject.SetActive(true);
            OnProcessStart();
            onStart?.Invoke();
        }

        public void Finish()
        {
            OnProcessFinish();
            gameObject.SetActive(false);
            onFinish?.Invoke();
        }

        public abstract void OnProcessStart();

        public virtual void OnAwake() { }

        public virtual void OnStart() { }

        public virtual void OnActionUpdate() { }

        public virtual void OnUpdate() { }

        public virtual void OnActionFixUpdate() { }

        public virtual void OnFixUpdate() { }

        public virtual void OnProcessFinish() { }

        public virtual void OnProcessForceStop() { }

        public virtual void OnProcessDestroy() { }



        #region Unity Event

        private void Awake() 
        {
            OnAwake();
        }

        private void Start() 
        {
            OnStart();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            OnUpdate();
            if (gameObject.activeSelf)
                OnActionUpdate();
        }

        private void FixedUpdate()
        {
            OnFixUpdate();
            if (gameObject.activeSelf)
                OnActionFixUpdate();
        }

        private void OnDestroy()
        {
            OnProcessForceStop();
            OnProcessDestroy();
        }

        #endregion
    }
}

