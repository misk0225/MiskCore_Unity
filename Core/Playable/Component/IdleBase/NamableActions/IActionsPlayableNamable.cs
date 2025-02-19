

namespace MiskCore.Playables.Module.IdleBase.Namble
{
    /// <summary>
    /// 可被稱呼的 Action 行為
    /// </summary>
    public interface IActionsPlayableNamable
    {

        /// <summary>
        /// 用名稱獲得一個行為
        /// </summary>
        public IActionOncePlayable GetActionPlayable(string actionName);
    }

}
