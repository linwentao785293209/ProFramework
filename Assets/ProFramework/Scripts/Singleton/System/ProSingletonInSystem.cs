using System;
using System.Reflection;

namespace ProFramework
{
    /// <summary>
    /// C#系统的单例模式基类，主要目的是避免代码的冗余，方便实现单例模式的类
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public abstract class ProSingletonInSystem<T> where T : class
    {
        // 判断单例模式对象是否为null
        protected bool InstanceIsNull => instance == null;

        // 用于加锁的对象
        protected static readonly object lockObj = new object();

        // 单例实例
        private static T instance;


        /// <summary>
        /// 属性的方式获取单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            // 利用反射得到无参私有的构造函数来实例化对象
                            Type type = typeof(T);
                            ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                null,
                                Type.EmptyTypes,
                                null);
                            if (info != null)
                            {
                                instance = info.Invoke(null) as T;
                            }
                            else
                            {
                                ProLog.LogError("未能获取对应的无参构造函数");
                            }

                            // 另一种反射方式使用构造函数
                            // instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                    }
                }

                return instance;
            }
        }
    }
}