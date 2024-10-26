using System;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// �̳�MonoBehaviour�ֶ�����ʽ����ģʽ����
    /// </summary>
    /// <typeparam name="T">����������</typeparam>
    public class ProUnityManualSingleton<T> : MonoBehaviour where T : ProUnityManualSingleton<T>
    {
        private static readonly object _lockObject = new object();

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();
                            if (_instance == null)
                            {
                                ProLog.LogError($"δ���ҵ����صĽű� {typeof(T)} ��");
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
                ProLog.LogError($"���������Ѵ��ڣ����ܶദ�ظ����� {typeof(T)} ��ʵ����");
                throw new InvalidOperationException($"{typeof(T)} ���͵ĵ��������Ѵ��ڣ����ܶദ�ظ����ػ�̬��ӵĽű���");
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}