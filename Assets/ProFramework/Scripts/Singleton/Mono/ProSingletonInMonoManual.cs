using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 继承Mono的单例模式基类 需要手动挂载到对象上
    /// </summary>
    /// <typeparam name="T">单例的类型</typeparam>
    public class ProSingletonInMonoManual<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// 单例实例
        private static T instance;

        /// <summary>
        /// 属性的方式获取单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                // 如果实例为null，尝试在场景中找到它
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        ProLog.LogError("未能找到挂载的脚本");
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 当脚本被唤醒时执行
        /// </summary>
        protected virtual void Awake()
        {
            // 如果已经存在一个对应的单例模式对象了，不需要再创建一个
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            // 将当前实例赋值给单例实例
            instance = this as T;

            // 挂载继承该单例模式基类的脚本后，依附的对象过场景时就不会被移除
            // 这样可以保证在游戏的整个生命周期中都存在
            DontDestroyOnLoad(this.gameObject);
        }
    }
}