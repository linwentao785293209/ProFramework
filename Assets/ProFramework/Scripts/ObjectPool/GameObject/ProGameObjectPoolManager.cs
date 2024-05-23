using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 游戏对象池管理器
    /// </summary>
    public class ProGameObjectPoolManager : ProSingletonInSystem<ProGameObjectPoolManager>
    {
        /// <summary>
        /// 游戏对象池信息字典
        /// </summary>
        private Dictionary<string, ProGameObjectPool> _gameObjectPoolDictionary =
            new Dictionary<string, ProGameObjectPool>();

        /// <summary>
        /// 游戏对象池根节点
        /// </summary>
        private GameObject _gameObjectPoolRootNode;

        /// <summary>
        /// 是否开启游戏对象池布局优化
        /// </summary>
        public static bool isOpenGameObjectPoolLayoutOptimization = true;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ProGameObjectPoolManager()
        {
            if (_gameObjectPoolRootNode == null && isOpenGameObjectPoolLayoutOptimization)
            {
                _gameObjectPoolRootNode = new GameObject("[GameObjectPoolRoot]Node");
            }
        }

        /// <summary>
        /// 获取游戏对象
        /// </summary>
        /// <param name="resourceName">对象路径</param>
        /// <returns>游戏对象</returns>
        public void Get(string assetBundleName, string resourceName, UnityAction<GameObject> callBack = null,
            bool isSync = false)
        {
            if (_gameObjectPoolRootNode == null && isOpenGameObjectPoolLayoutOptimization)
            {
                _gameObjectPoolRootNode = new GameObject("[GameObjectPoolRoot]Node");
            }

            GameObject gameObject = null;

            if (!_gameObjectPoolDictionary.ContainsKey(resourceName) ||
                _gameObjectPoolDictionary[resourceName].NeedCreateNewGameObject)
            {
                ProAssetManager.Instance.LoadResource<GameObject>(assetBundleName, resourceName, (resource =>
                {
                    // 如果对象池中不存在该路径的对象，或者需要创建新的对象
                    gameObject = GameObject.Instantiate(resource);

                    gameObject.name = resourceName;

                    if (!_gameObjectPoolDictionary.ContainsKey(resourceName))
                    {
                        _gameObjectPoolDictionary.Add(resourceName,
                            new ProGameObjectPool(_gameObjectPoolRootNode, resourceName));
                    }

                    _gameObjectPoolDictionary[resourceName].PushGameObjectToUsedList(gameObject);

                    callBack?.Invoke(gameObject);
                }),isSync);
            }
            else
            {
                // 从对象池中获取对象
                gameObject = _gameObjectPoolDictionary[resourceName].Get();
                
                callBack?.Invoke(gameObject);
            }
        }

        /// <summary>
        /// 将游戏对象放回对象池
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        public void Push(GameObject gameObject)
        {
            if (_gameObjectPoolDictionary.ContainsKey(gameObject.name))
            {
                _gameObjectPoolDictionary[gameObject.name].Push(gameObject);
            }
            else
            {
                ProLog.LogWarning("传进来的游戏对象没有对象池，或者名字和路径不匹配，不能放回对象池！");
            }
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void Clear()
        {
            _gameObjectPoolDictionary.Clear();
            _gameObjectPoolRootNode = null;
        }
    }
}