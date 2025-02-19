using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiskCore.Playables.Module.IdleBase;
using MiskCore.Playables.Module.IdleBase.StateMachine;
using System;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    public class CharacterStateController
    {
        /// <summary>
        /// 用來控制角色狀態
        /// </summary>
        private IdleBaseStateController _IdleBaseStateController;

        /// <summary>
        /// 主要控制的 Component
        /// 這個專案期望做動作之外保持 Idle 狀態，所以使用 IdleBase
        /// </summary>
        private IdleBaseComponent _Component;

        /// <summary>
        /// 最初狀態
        /// </summary>
        private ICharacterPlayableRootStateInfo _Root;

        /// <summary>
        /// 當前狀態
        /// </summary>
        public ICharacterPlayableStateInfo CurState { get; private set; }

        /// <summary>
        /// 當前可用稱呼獲得行為的介面
        /// </summary>
        public IActionsPlayableNamable NambleActions { get; private set; }

        /// <summary>
        /// 所有角色狀態
        /// </summary>
        private Dictionary<int, ICharacterPlayableStateInfo> _States = new Dictionary<int, ICharacterPlayableStateInfo>();



        public CharacterStateController (IdleBaseComponent component)
        {
            _Component = component;
            _IdleBaseStateController = new IdleBaseStateController(component);
        }
        public CharacterStateController (IdleBaseComponent component, ICharacterPlayableRootStateInfo root) : this(component)
        {
            SetRootState(root);
        }



        public void SetRootState(ICharacterPlayableRootStateInfo root)
        {
            _Root = root;
            _IdleBaseStateController.SetRoot(root.IdlePlayable);
        }


        public bool TryAddStateInfo(ICharacterPlayableStateInfo info)
        {
            if (_States.ContainsKey(info.Type))
                return false;

            _States.Add(info.Type, info);
            return true;
        }


        public void ActionByName(string name, Action onFinish = null)
        {
            if (NambleActions != null)
            {
                IActionOncePlayable action = NambleActions.GetActionPlayable(name);
                _Component.ActionOnceAnimation(action, onFinish);
            }
        }


        public void SwitchState(int type, float mixTime, Action onFinish = null)
        {
            ICharacterPlayableStateInfo state = _States[type];
            _IdleBaseStateController.SwitchState(state.StateInfo, mixTime, onFinish);
            NambleActions = state.NamableActions;
        }


        public void ResetToNormal(float mixTime, Action onFinish = null)
        {
            _IdleBaseStateController.ResetToRoot(mixTime, onFinish);
            NambleActions = _Root.NamableActions;
        }

    }

}
