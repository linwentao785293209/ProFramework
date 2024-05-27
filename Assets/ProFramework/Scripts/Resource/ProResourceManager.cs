using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ProFramework
{
    /// <summary>
    /// Resources 资源加载模块管理器
    /// </summary>
    public class ProResourceManager : ProSingletonInSystem<ProResourceManager>, IProLoadResourceManager
    {
        //用于存储加载过的资源或者加载中的资源的容器
        private Dictionary<string, ProAbstractResourceInfo> _resourceDictionary =
            new Dictionary<string, ProAbstractResourceInfo>();

        private ProResourceManager()
        {
        }


        public void LoadResource<T>(string assetBundleName, string resourceName, UnityAction<T> callBack = null,
            bool isSync = false) where T : UnityEngine.Object
        {
            string path = $"{assetBundleName}/{resourceName}";

            if (isSync)
            {
                LoadSync(path, callBack);
            }
            else
            {
                LoadAsync(path, callBack);
            }
        }

        /// <summary>
        /// 同步加载Resources下资源的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadSync<T>(string path, UnityAction<T> callBack = null) where T : UnityEngine.Object
        {
            string resourceName = path + "_" + typeof(T).Name;

            ProResourceInfo<T> resourceInfo;

            //字典中不存在资源时
            if (!_resourceDictionary.ContainsKey(resourceName))
            {
                //直接同步加载 并且记录资源信息 到字典中 方便下次直接取出来用
                T resource = Resources.Load<T>(path);
                resourceInfo = new ProResourceInfo<T>
                {
                    asset = resource
                };
                //引用计数增加
                resourceInfo.AddRefCount();
                _resourceDictionary.Add(resourceName, resourceInfo);

                callBack?.Invoke(resourceInfo.asset);
                return resource;
            }
            else
            {
                //取出字典中的记录
                resourceInfo = _resourceDictionary[resourceName] as ProResourceInfo<T>;

                //引用计数增加
                resourceInfo.AddRefCount();

                //存在异步加载 还在加载中
                if (resourceInfo.asset == null)
                {
                    //停止异步加载 
                    ProMonoManager.Instance.StopCoroutine(resourceInfo.coroutine);
                    //直接采用同步的方式加载成功
                    T resource = Resources.Load<T>(path);
                    //记录 
                    resourceInfo.asset = resource;
                    //还应该把那些等待着异步加载结束的委托去执行了
                    resourceInfo.callBack?.Invoke(resource);
                    //回调结束 异步加载也停了 所以清除无用的引用
                    resourceInfo.callBack = null;
                    resourceInfo.coroutine = null;

                    callBack?.Invoke(resourceInfo.asset);
                    // 并使用
                    return resource;
                }
                else
                {
                    callBack?.Invoke(resourceInfo.asset);
                    //如果已经加载结束 直接用
                    return resourceInfo.asset;
                }
            }
        }


        /// <summary>
        /// 异步加载资源的方法
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">资源路径（Resources下的）</param>
        /// <param name="callBack">加载结束后的回调函数 当异步加载资源结束后才会调用</param>
        public void LoadAsync<T>(string path, UnityAction<T> callBack) where T : UnityEngine.Object
        {
            //资源的唯一ID，是通过 路径名_资源类型 拼接而成的
            string resourceName = path + "_" + typeof(T).Name;

            ProResourceInfo<T> resourceInfo;

            if (!_resourceDictionary.ContainsKey(resourceName))
            {
                //声明一个 资源信息对象
                resourceInfo = new ProResourceInfo<T>();
                //引用计数增加
                resourceInfo.AddRefCount();
                //将资源记录添加到字典中（资源还没有加载成功）
                _resourceDictionary.Add(resourceName, resourceInfo);
                //记录传入的委托函数 一会儿加载完成了 再使用
                resourceInfo.callBack += callBack;
                //开启协程去进行 异步加载 并且记录协同程序 （用于之后可能的 停止）
                resourceInfo.coroutine = ProMonoManager.Instance.StartCoroutine(LoadAsyncCoroutine<T>(path));
            }
            else
            {
                //从字典中取出资源信息
                resourceInfo = _resourceDictionary[resourceName] as ProResourceInfo<T>;
                //引用计数增加
                resourceInfo.AddRefCount();
                //如果资源还没有加载完 
                //意味着 还在进行异步加载
                if (resourceInfo.asset == null)
                {
                    resourceInfo.callBack += callBack;
                }
                else
                {
                    callBack?.Invoke(resourceInfo.asset);
                }
            }
        }

        private IEnumerator LoadAsyncCoroutine<T>(string path) where T : UnityEngine.Object
        {
            //异步加载资源
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
            //等待资源加载结束后 才会继续执行yield return后面的代码
            yield return resourceRequest;

            string resourceName = path + "_" + typeof(T).Name;
            //资源加载结束 将资源传到外部的委托函数去进行使用
            if (_resourceDictionary.ContainsKey(resourceName))
            {
                ProResourceInfo<T> resourceInfo = _resourceDictionary[resourceName] as ProResourceInfo<T>;
                //取出资源信息 并且记录加载完成的资源
                resourceInfo.asset = resourceRequest.asset as T;

                //如果发现需要删除 再去移除资源
                //引用计数为0 才真正去移除
                if (resourceInfo.refCount == 0)
                    UnloadAsset<T>(path, resourceInfo.isDel, null, false);
                else
                {
                    //将加载完成的资源传递出去
                    resourceInfo.callBack?.Invoke(resourceInfo.asset);
                    //加载完毕后 这些引用就可以清空 避免引用的占用 可能带来的潜在的内存泄漏问题
                    resourceInfo.callBack = null;
                    resourceInfo.coroutine = null;
                }
            }
        }


        /// <summary>
        /// 指定卸载一个资源
        /// </summary>
        /// <param name="assetToUnload"></param>
        public void UnloadAsset<T>(string path, bool isDel = false, UnityAction<T> callBack = null, bool isSub = true) where T : UnityEngine.Object
        {
            string resourceName = path + "_" + typeof(T).Name;
            //判断是否存在对应资源
            if (_resourceDictionary.ContainsKey(resourceName))
            {
                ProResourceInfo<T> resourceInfo = _resourceDictionary[resourceName] as ProResourceInfo<T>;
                //引用计数-1
                if (isSub)
                    resourceInfo.SubRefCount();
                //记录 引用计数为0时  是否马上移除标签
                resourceInfo.isDel = isDel;
                //资源已经加载结束 
                if (resourceInfo.asset != null && resourceInfo.refCount == 0 && resourceInfo.isDel)
                {
                    //从字典移除
                    _resourceDictionary.Remove(resourceName);

                    // 如果资源类型是 GameObject，则不执行资源卸载操作
                    if (!(resourceInfo.asset is GameObject))
                    {
                        //通过api 卸载资源
                        Resources.UnloadAsset(resourceInfo.asset);
                    }
                    else
                    {
                        ProLog.LogWarning("不能卸载GameObject资源！");
                    }
                }
                else if (resourceInfo.asset == null) //资源正在异步加载中
                {
                    //当异步加载不想使用时 我们应该移除它的回调记录 而不是直接去卸载资源
                    if (callBack != null)
                        resourceInfo.callBack -= callBack;
                }
            }
        }


        /// <summary>
        /// 异步卸载对应没有使用的Resources相关的资源
        /// </summary>
        /// <param name="callBack">回调函数</param>
        public void UnloadUnusedAssets(UnityAction callBack)
        {
            ProMonoManager.Instance.StartCoroutine(UnloadUnusedAssetsCoroutine(callBack));
        }

        private IEnumerator UnloadUnusedAssetsCoroutine(UnityAction callBack)
        {
            //就是在真正移除不使用的资源之前 应该把我们自己记录的那些引用计数为0 并且没有被移除记录的资源
            //移除掉
            List<string> unloadList = new List<string>();
            foreach (string path in _resourceDictionary.Keys)
            {
                if (_resourceDictionary[path].refCount == 0)
                    unloadList.Add(path);
            }

            foreach (string path in unloadList)
            {
                _resourceDictionary.Remove(path);
            }

            AsyncOperation asyncOperation = Resources.UnloadUnusedAssets();
            yield return asyncOperation;
            //卸载完毕后 通知外部
            callBack();
        }

        /// <summary>
        /// 获取当前某个资源的引用计数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetRefCount<T>(string path)
        {
            string resourceName = path + "_" + typeof(T).Name;
            if (_resourceDictionary.ContainsKey(resourceName))
            {
                return (_resourceDictionary[resourceName] as ProResourceInfo<T>).refCount;
            }

            return 0;
        }


        /// <summary>
        /// 清空字典
        /// </summary>
        /// <param name="callBack"></param>
        public void Clear(UnityAction callBack)
        {
            ProMonoManager.Instance.StartCoroutine(ClearCoroutine(callBack));
        }

        private IEnumerator ClearCoroutine(UnityAction callBack)
        {
            _resourceDictionary.Clear();
            AsyncOperation asyncOperation = Resources.UnloadUnusedAssets();
            yield return asyncOperation;
            //卸载完毕后 通知外部
            callBack();
        }
    }
}