using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 继承Mono的单例模式基类 推荐使用 自动挂载式到对象上 无需手动挂载 无需动态添加 无需关心切场景带来的问题
    /// </summary>
    /// <typeparam name="T">单例的类型</typeparam>
    public class ProSingletonInMonoAuto<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        private static T instance;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 尝试在场景中查找具有类型 T 的对象
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        // 动态创建，动态挂载
                        // 在场景上创建空物体
                        GameObject gameObj = new GameObject();
                        // 得到T脚本的类名，为对象改名，这样在编辑器中可以明确地看到该
                        // 单例模式脚本对象依附的GameObject
                        gameObj.name = typeof(T).ToString();
                        // 动态挂载对应的 单例模式脚本
                        instance = gameObj.AddComponent<T>();
                        // 过场景时不移除对象，保证它在整个游戏生命周期中都存在
                        DontDestroyOnLoad(gameObj);
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
            // 如果单例实例尚未创建
            if (instance == null)
            {
                // 将当前对象实例赋值给 instance
                instance = this as T;

                // 不要在场景切换时销毁该对象
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                // 如果单例实例已存在，则销毁当前对象
                Destroy(this);
            }
        }
    }
}