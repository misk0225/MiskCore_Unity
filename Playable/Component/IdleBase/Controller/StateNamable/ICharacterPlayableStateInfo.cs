using MiskCore.Playables.Module.IdleBase.StateMachine;
using MiskCore.Playables.Module.IdleBase;

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// ���⪺���A��T
    /// �C�Ӫ��A���i�H���U�۪��ʧ@�A�ʧ@�ѦW����o
    /// </summary>
    public interface ICharacterPlayableStateInfo
    {

        /// <summary>
        /// ���A����
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// ���A���
        /// </summary>
        public IIdleBaseStateInfo StateInfo { get; }

        /// <summary>
        /// �����A�U�i�Q�٩I�� Action �欰
        /// </summary>
        public IActionsPlayableNamable NamableActions { get; }
    }
}

