using MiskCore.Playables.Module.IdleBase.StateMachine;
using MiskCore.Playables.Module.IdleBase;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// 角色的狀態資訊
    /// 每個狀態都可以做各自的動作，動作由名稱獲得
    /// </summary>
    public interface ICharacterPlayableStateInfo
    {

        /// <summary>
        /// 狀態種類
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// 狀態資料
        /// </summary>
        public IIdleBaseStateInfo StateInfo { get; }

        /// <summary>
        /// 此狀態下可被稱呼的 Action 行為
        /// </summary>
        public IActionsPlayableNamable NamableActions { get; }
    }
}

