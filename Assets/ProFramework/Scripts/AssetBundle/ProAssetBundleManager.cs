using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;


namespace ProFramework
{
    public class ProAssetBundleManager : ProSingletonInMonoAuto<ProAssetBundleManager>, IProLoadResourceManager
    {
        //主包
        private AssetBundle _mainAssetBundle = null;

        //主包依赖获取配置文件
        private AssetBundleManifest _assetBundleManifest = null;

        //选择存储 AB包的容器
        //AB包不能够重复加载 否则会报错
        //字典用来存储 AB包对象
        private Dictionary<string, AssetBundle> _assetBundleDictionary = new Dictionary<string, AssetBundle>();


        private string streamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.AssetBundlePath}";

        private string persistentDataPath => $"{Application.persistentDataPath}/{ProConst.AssetBundlePath}";


        /// <summary>
        /// 主包名 根据平台不同 报名不同
        /// </summary>
        private string mainAssetBundleName
        {
            get
            {
            #if UNITY_IOS
                return "IOS";
            #elif UNITY_ANDROID
                return "Android";
            #else
                return "PC";
            #endif
            }
        }

        /// <summary>
        /// 加载主包 和 配置文件
        /// 因为加载所有包是 都得判断 通过它才能得到依赖信息
        /// 所以写一个方法
        /// </summary>
        private void LoadMainAssetBundleAndAssetBundleManifest()
        {
            if (_mainAssetBundle == null)
            {
                string mainAssetBundlePath = File.Exists(persistentDataPath + mainAssetBundleName)
                    ? persistentDataPath + mainAssetBundleName
                    : streamingAssetsPath + mainAssetBundleName;

                _mainAssetBundle = AssetBundle.LoadFromFile(mainAssetBundlePath);
                _assetBundleManifest = _mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
        }


        /// <summary>
        /// 泛型异步加载资源
        /// </summary>
        /// <typeparam assetBundleName="T"></typeparam>
        /// <param assetBundleName="assetBundleName"></param>
        /// <param assetBundleName="resourceName"></param>
        /// <param assetBundleName="callBack"></param>
        public void LoadResource<T>(string assetBundleName, string resourceName, UnityAction<T> callBack,
            bool isSync = false)
            where T : Object
        {
            StartCoroutine(LoadResourceCoroutine<T>(assetBundleName, resourceName, callBack, isSync));
        }

        //正儿八经的 协程函数
        private IEnumerator LoadResourceCoroutine<T>(string assetBundleName, string resourceName,
            UnityAction<T> callBack,
            bool isSync)
            where T : Object
        {
            //加载主包
            LoadMainAssetBundleAndAssetBundleManifest();

            //获取依赖包
            string[] strs = _assetBundleManifest.GetAllDependencies(assetBundleName);

            //遍历依赖包并加载
            for (int i = 0; i < strs.Length; i++)
            {
                //还没有加载过该AB包
                if (!_assetBundleDictionary.ContainsKey(strs[i]))
                {
                    string assetBundlePath = File.Exists(persistentDataPath + strs[i])
                        ? persistentDataPath + strs[i]
                        : streamingAssetsPath + strs[i];

                    //同步加载
                    if (isSync)
                    {
                        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
                        _assetBundleDictionary.Add(strs[i], assetBundle);
                        ProLog.LogDebug($"依赖包{strs[i]}同步加载成功！");
                    }
                    //异步加载
                    else
                    {
                        //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
                        _assetBundleDictionary.Add(strs[i], null);
                        ProLog.LogDebug($"依赖包{strs[i]}开始异步加载！");

                        AssetBundleCreateRequest assetBundleCreateRequest =
                            AssetBundle.LoadFromFileAsync(assetBundlePath);
                        yield return assetBundleCreateRequest;

                        ProLog.LogDebug($"依赖包{strs[i]}异步加载成功！");
                        //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
                        _assetBundleDictionary[strs[i]] = assetBundleCreateRequest.assetBundle;
                    }
                }
                //就证明 字典中已经记录了一个AB包相关信息了
                else
                {
                    //如果字典中记录的信息是null 那就证明正在加载中
                    //我们只需要等待它加载结束 就可以继续执行后面的代码了
                    while (_assetBundleDictionary.ContainsKey(strs[i]) && _assetBundleDictionary[strs[i]] == null)
                    {
                        ProLog.LogDebug($"依赖包{strs[i]}已经在异步加载了，等待异步加载成功！");
                        //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                        yield return 0;
                    }

                    if (!_assetBundleDictionary.ContainsKey(strs[i]))
                    {
                        ProLog.LogError($"依赖包{strs[i]}在被其他地方卸载，加载失败返回空，请检查逻辑！");
                        yield break;
                    }
                }
            }

            //加载目标包
            if (!_assetBundleDictionary.ContainsKey(assetBundleName))
            {
                string assetBundlePath = File.Exists(persistentDataPath + assetBundleName)
                    ? persistentDataPath + assetBundleName
                    : streamingAssetsPath + assetBundleName;

                //同步加载
                if (isSync)
                {
                    AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
                    _assetBundleDictionary.Add(assetBundleName, assetBundle);
                    ProLog.LogDebug($"目标包{assetBundleName}同步加载成功！");
                }
                else
                {
                    //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
                    _assetBundleDictionary.Add(assetBundleName, null);
                    ProLog.LogDebug($"目标包{assetBundleName}开始异步加载！");
                    AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
                    yield return assetBundleCreateRequest;
                    //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
                    _assetBundleDictionary[assetBundleName] = assetBundleCreateRequest.assetBundle;
                    ProLog.LogDebug($"目标包{assetBundleName}异步加载成功！");
                }
            }
            else
            {
                //如果字典中记录的信息是null 那就证明正在加载中
                //我们只需要等待它加载结束 就可以继续执行后面的代码了
                while (_assetBundleDictionary.ContainsKey(assetBundleName) && _assetBundleDictionary[assetBundleName] == null)
                {
                    ProLog.LogDebug($"目标包{assetBundleName}已经在异步加载了，等待异步加载成功！");
                    //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                    yield return 0;
                }
                
                if (!_assetBundleDictionary.ContainsKey(assetBundleName))
                {
                    ProLog.LogError($"目标包{assetBundleName}在被其他地方卸载，加载失败返回空，请检查逻辑！");
                    yield break;
                }
            }

            //同步加载AB包中的资源
            if (isSync)
            {
                //即使是同步加载 也需要使用回调函数传给外部进行使用
                T resource = _assetBundleDictionary[assetBundleName].LoadAsset<T>(resourceName);
                callBack(resource);
            }
            //异步加载包中资源
            else
            {
                AssetBundleRequest assetBundleRequest =
                    _assetBundleDictionary[assetBundleName].LoadAssetAsync<T>(resourceName);
                yield return assetBundleRequest;

                callBack(assetBundleRequest.asset as T);
            }
        }


        //卸载AB包的方法
        public void UnLoadAssetBundle(string assetBundleName, UnityAction<bool> callBack = null,
            bool unloadAllLoadedObjects = false)
        {
            if (_assetBundleDictionary.ContainsKey(assetBundleName))
            {
                if (_assetBundleDictionary[assetBundleName] == null)
                {
                    ProLog.LogDebug($"{assetBundleName}包正在异步加载，卸载失败！");
                    //代表正在异步加载 没有卸载成功
                    callBack(false);
                    return;
                }

                if (HasDependencies(assetBundleName))
                {
                    ProLog.LogDebug($"{assetBundleName}包有被其他未卸载的包依赖，卸载失败！");
                    callBack(false);
                    return;
                }

                _assetBundleDictionary[assetBundleName].Unload(unloadAllLoadedObjects);
                _assetBundleDictionary.Remove(assetBundleName);
                ProLog.LogDebug($"{assetBundleName}成功卸载！");
                //卸载成功
                callBack(true);
            }
            else
            {
                ProLog.LogWarning($"{assetBundleName}包没有被加载，无需卸载！");
            }
        }

        private bool HasDependencies(string assetBundleName)
        {
            // 遍历字典中除了自己的其他 AssetBundle
            foreach (var kvp in _assetBundleDictionary)
            {
                // 跳过当前 AssetBundle
                if (kvp.Key == assetBundleName)
                    continue;

                // 获取当前 AssetBundle 的所有依赖
                string[] dependencies = _assetBundleManifest.GetAllDependencies(kvp.Key);

                // 检查当前 AssetBundle 是否依赖于要卸载的 AssetBundle
                foreach (var dependency in dependencies)
                {
                    if (dependency == assetBundleName)
                    {
                        // 发现有依赖
                        return true;
                    }
                }
            }

            // 没有找到依赖
            return false;
        }

        
        //清空AB包的方法
        public void ClearAssetBundle(bool unloadAllLoadedObjects = false)
        {
            //由于AB包都是异步加载了 因此在清理之前 停止协同程序
            StopAllCoroutines();
            AssetBundle.UnloadAllAssetBundles(unloadAllLoadedObjects);
            _assetBundleDictionary.Clear();
            //卸载主包
            _mainAssetBundle = null;
        }
    }
}