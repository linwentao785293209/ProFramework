using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 有参事件信息 用来包裹对应观察者函数委托的类
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    internal class ProEventInfo<T> : IProEventInfo
    {
        /// <summary>
        /// 观察者对应的函数委托
        /// </summary>
        public UnityAction<T> actions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action">委托</param>
        public ProEventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    /// <summary>
    /// 无参事件信息 主要用来记录无参无返回值委托
    /// </summary>
    internal class ProEventInfo : IProEventInfo
    {
        /// <summary>
        /// 无参无返回值委托
        /// </summary>
        public UnityAction actions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action">委托</param>
        public ProEventInfo(UnityAction action)
        {
            actions += action;
        }
    }
}