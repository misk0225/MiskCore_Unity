

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// �i�Q�٩I�� Action �欰
    /// </summary>
    public interface IActionsPlayableNamable
    {

        /// <summary>
        /// �ΦW����o�@�Ӧ欰
        /// </summary>
        public IActionOncePlayable GetActionPlayable(string actionName);
    }

}
