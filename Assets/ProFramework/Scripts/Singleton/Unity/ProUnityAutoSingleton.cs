using System;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// �̳�MonoBehaviour�Զ�����ʽ����ģʽ����
    /// </summary>
    /// <typeparam name="T">����������</typeparam>
    public class ProUnityAutoSingleton<T> : MonoBehaviour where T : ProUnityAutoSingleton<T>
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
                    Debug.LogWarning($"{typeof(T).Name} ���������˳��ڼ䱻���٣��޷���ȡʵ����");
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
                                GameObject gameObj = new GameObject
                                {
                                    name = typeof(T).Name
                                };
                                _instance = gameObj.AddComponent<T>();
                                DontDestroyOnLoad(gameObj);
                            }
                        }
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // ��ֹ���ض����ǰ�ű��Ͷ�̬���
            if (_instance == null)
            {
                _instance = this as T;
                this.gameObject.name = typeof(T).Name;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                DestroyImmediate(this.gameObject);
                ProLog.LogError($"���������Ѵ��ڣ������ظ����ػ�̬��� {typeof(T)} ��ʵ����");
                throw new InvalidOperationException($"{typeof(T)} ���͵ĵ��������Ѵ��ڣ������ظ����ػ�̬��ӵĽű���");
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