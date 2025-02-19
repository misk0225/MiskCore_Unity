using MiskCore.Playables.Module.IdleBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Playables.Module.IdleBase.Namble
{
    public class NamableIdleBaseComponent : IdleBaseComponent
    {
        [SerializeField]
        private IdlePlayableScriptableObject _DefaultIdle;

        [SerializeField]
        private NamableActionScriptableObject _DefaultNambleAction;

        /// <summary>
        /// 當前可用稱呼獲得行為的介面
        /// </summary>
        private IActionsPlayableNamable _NambleActions;


        protected override void Awake()
        {
            base.Awake();

            if (_DefaultIdle != null) SetIdlePlayable(_DefaultIdle.GetIdle(Graph));
            if (_DefaultNambleAction != null) SetNamableAction(_DefaultNambleAction.GetNamableAction(Graph));
        }



        public void SetNamableAction (IActionsPlayableNamable actions)
        {
            _NambleActions = actions;
        }


        public void ActionByName(string name) => ActionByName(name, null, 1);
        public void ActionByName(string name, float speed) => ActionByName(name, null, speed);
        public void ActionByName(string name, Action onFinish, float speed = 1f)
        {
            if (_NambleActions == null) return;

            IActionOncePlayable action = _NambleActions.GetActionPlayable(name);
            if (action == null) return;

            ActionOnceAnimation(action, onFinish, speed);
        }
    }
}

