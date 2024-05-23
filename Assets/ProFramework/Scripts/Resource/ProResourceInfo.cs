using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 资源信息对象 主要用于存储资源信息 异步加载委托信息 异步加载 协程信息
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    public class ProResourceInfo<T> : ProAbstractResourceInfo
    {
        //资源
        public T asset;

        //主要用于异步加载结束后 传递资源到外部的委托
        public UnityAction<T> callBack;

        //用于存储异步加载时 开启的协同程序
        public Coroutine coroutine;

        //决定引用计数为0时 是否真正需要移除
        public bool isDel;


        public void AddRefCount()
        {
            ++refCount;
        }

        public void SubRefCount()
        {
            --refCount;
            if (refCount < 0)
            {
                ProLog.LogError("引用计数小于0了，请检查使用和卸载是否配对执行");
            }
               
        }
    }
}