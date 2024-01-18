using System;
using System.Collections.Generic;
using UniRx;

namespace MiskCore
{
    /// <summary>
    /// 單線流程以事件串接流程, 是以堆入的順序執行, 先前先出, 特殊情況可以用 Insert 加入在開頭位置
    /// </summary>
    public class ActionQueue
    {
        // UniRx Disposable集合工具類,用來存放有需要延遲事件
        CompositeDisposable m_Disposables = new CompositeDisposable();

        LinkedList<Action<ActionQueue>> m_Actions = new LinkedList<Action<ActionQueue>>();

        /// <summary>
        /// 是否為空佇列
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return m_Actions.Count == 0;
        }

        /// <summary>
        /// 執行下一個堆疊中事件
        /// </summary>
        public void Next()
        {
            if (m_Actions.Count > 0)
            {
                var item = m_Actions.First.Value;
                m_Actions.RemoveFirst();
                item.Invoke(this);
            }
        }

        /// <summary>
        /// 產生一個延遲時間事件
        /// </summary>
        /// <param name="seconds">延遲秒數</param>
        public void DelayTime(float seconds)
        {
            Push((instance) =>
            {
                Observable.Timer(TimeSpan.FromSeconds(seconds)).Subscribe(_ => instance.Next()).AddTo(instance.m_Disposables);
            });
        }

        /// <summary>
        /// 產生一個延遲時間事件在佇列的最前端
        /// </summary>
        /// <param name="seconds">延遲秒數</param>
        public void InsertDelayTime(float seconds)
        {
            InsertFirst((instance) =>
            {
                Observable.Timer(TimeSpan.FromSeconds(seconds)).Subscribe(_ => instance.Next()).AddTo(instance.m_Disposables);
            });
        }

        /// <summary>
        /// 堆入一個等待執行事件
        /// </summary>
        /// <param name="action"></param>
        public void Push(Action action)
        {
            m_Actions.AddLast((instance) => action());
        }
        public void Push(Action<ActionQueue> action)
        {
            m_Actions.AddLast((instance) => action(instance));
        }

        /// <summary>
        /// 插入一個等待執行事件到佇列最前端
        /// </summary>
        /// <param name="action"></param>
        public void InsertFirst(Action action)
        {
            m_Actions.AddFirst((instance) => action());
        }
        public void InsertFirst(Action<ActionQueue> action)
        {
            m_Actions.AddFirst((instance) => action(instance));
        }

        /// <summary>
        /// 清空所有尚未處理事件
        /// </summary>
        public void Clear()
        {
            m_Actions.Clear();
            m_Disposables.Clear();
        }

        /// <summary>
        /// 取得佇列剩於數量
        /// </summary>
        /// <returns></returns>
        public int WaitCount()
        {
            return m_Actions.Count;
        }


        public ActionQueue Clone()
        {
            ActionQueue q = new ActionQueue();
            q.m_Actions = new LinkedList<Action<ActionQueue>>(m_Actions);
            q.m_Disposables = new CompositeDisposable();

            return q;
        }
    }
}


