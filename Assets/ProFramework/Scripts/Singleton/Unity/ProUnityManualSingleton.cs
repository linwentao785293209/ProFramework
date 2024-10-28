using System;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 继承MonoBehaviour手动挂载式单例模式基类
    /// </summary>
    /// <typeparam name="T">单例的类型</typeparam>
    public class ProUnityManualSingleton<T> : MonoBehaviour where T : ProUnityManualSingleton<T>
    {
        private static readonly object _lockObject = new object();
        private static T _instance;
        private static bool _isApplicationQuitting = false;

        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                {
                    Debug.LogWarning($"{typeof(T).Name} 单例已在退出期间被销毁，无法获取实例。");
                    return null;
                }
                
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                ProLog.LogError($"未能找到挂载的脚本 {typeof(T)} 。");
                            }
                        }
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // 防止挂载多个当前脚本和动态添加
            if (_instance == null)
            {
                _instance = this as T;
                this.gameObject.name = typeof(T).Name;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this.gameObject);
                ProLog.LogError($"单例对象已存在，不能多处重复挂载 {typeof(T)} 的实例。");
                throw new InvalidOperationException($"{typeof(T)} 类型的单例对象已存在，不能多处重复挂载或动态添加的脚本。");
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        
        protected void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }
    }
}