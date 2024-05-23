using System.Collections.Generic;

namespace ProFramework
{
    /// <summary>
    /// 泛型系统对象池 用于存储 数据结构类 和 逻辑类 （不继承mono的）容器类
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    internal class ProSystemObjectPool<T> : IProSystemObjectPool where T : class, IProSystemObject, new()
    {
        /// <summary>
        /// 系统对象队列
        /// </summary>
        private Queue<T> _systemObjectQueue = new Queue<T>();

        /// <summary>
        /// 对象池中的对象数量
        /// </summary>
        public int Count => _systemObjectQueue.Count;

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>
        public T Get()
        {
            return _systemObjectQueue.Dequeue();
        }

        /// <summary>
        /// 将对象放回对象池
        /// </summary>
        /// <param name="systemObject">对象</param>
        public void Push(T systemObject)
        {
            _systemObjectQueue.Enqueue(systemObject);
        }
    }
}