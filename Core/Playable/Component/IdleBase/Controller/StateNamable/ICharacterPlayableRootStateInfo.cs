using MiskCore.Playables.Module.IdleBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// 角色的最初狀態
    /// </summary>
    public interface ICharacterPlayableRootStateInfo
    {

        public IIdlePlayable IdlePlayable { get; }


        public IActionsPlayableNamable NamableActions { get; }

    }
}

