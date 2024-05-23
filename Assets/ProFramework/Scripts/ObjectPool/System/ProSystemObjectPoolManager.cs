using System.Collections.Generic;

namespace ProFramework
{
    /// <summary>
    /// 系统对象池管理器
    /// </summary>
    public class ProSystemObjectPoolManager : ProSingletonInSystem<ProSystemObjectPoolManager>
    {
        /// <summary>
        /// 存储数据结构类、逻辑类对象的池子的字典容器
        /// </summary>
        private Dictionary<string, IProSystemObjectPool> _systemObjectPoolDictionary =
            new Dictionary<string, IProSystemObjectPool>();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ProSystemObjectPoolManager()
        {
        }

        /// <summary>
        /// 获取自定义的数据结构类和逻辑类对象 （不继承Mono的）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns>数据结构类或逻辑类对象</returns>
        public T Get<T>() where T : class, IProSystemObject, new()
        {
            // 池子的名字是根据类的类型来决定的，即类名
            string poolName = typeof(T).FullName;

            if (_systemObjectPoolDictionary.ContainsKey(poolName))
            {
                ProSystemObjectPool<T> systemObjectPool =
                    _systemObjectPoolDictionary[poolName] as ProSystemObjectPool<T>;

                if (systemObjectPool != null && systemObjectPool.Count > 0)
                {
                    // 从队列中取出对象进行复用
                    return systemObjectPool.Get();
                }
            }

            // 没有池子或池子为空，则新建对象
            T systemObject = new T();
            return systemObject;
        }

        /// <summary>
        /// 将自定义数据结构类和逻辑类对象放入池子中
        /// </summary>
        /// <typeparam name="T">对应类型</typeparam>
        /// <param name="systemObject">对象</param>
        public void Push<T>(T systemObject) where T : class, IProSystemObject, new()
        {
            if (systemObject == null)
                return;

            string poolName = typeof(T).FullName;

            ProSystemObjectPool<T> systemObjectPool;

            if (_systemObjectPoolDictionary.ContainsKey(poolName))
            {
                systemObjectPool = _systemObjectPoolDictionary[poolName] as ProSystemObjectPool<T>;

                if (systemObjectPool == null)
                {
                    _systemObjectPoolDictionary.Remove(poolName);
                    systemObjectPool = new ProSystemObjectPool<T>();
                    _systemObjectPoolDictionary.Add(poolName, systemObjectPool);
                }
            }
            else
            {
                systemObjectPool = new ProSystemObjectPool<T>();
                _systemObjectPoolDictionary.Add(poolName, systemObjectPool);
            }

            systemObject.ResetInfo();

            systemObjectPool.Push(systemObject);
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void Clear()
        {
            _systemObjectPoolDictionary.Clear();
        }
    }
}