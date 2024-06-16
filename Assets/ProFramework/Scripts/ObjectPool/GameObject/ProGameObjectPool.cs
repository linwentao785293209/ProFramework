using System.Collections.Generic;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 游戏对象池
    /// </summary>
    internal class ProGameObjectPool
    {
        /// <summary>
        /// 未使用的游戏对象栈
        /// </summary>
        private Stack<GameObject> _unusedGameObjectStack = new Stack<GameObject>();

        /// <summary>
        /// 已使用的游戏对象列表
        /// </summary>
        private List<GameObject> _usedGameObjectList = new List<GameObject>();

        /// <summary>
        /// 最大对象数量
        /// </summary>
        private int _maxNum = ProConst.GameObjectPoolDefaultMaxNum;

        /// <summary>
        /// 游戏对象池节点
        /// </summary>
        private GameObject _gameObjectPoolNode;

        /// <summary>
        /// 未使用的对象数量
        /// </summary>
        public int UnusedCount => _unusedGameObjectStack.Count;

        /// <summary>
        /// 已使用的对象数量
        /// </summary>
        public int UsedCount => _usedGameObjectList.Count;

        /// <summary>
        /// 是否需要创建新的游戏对象
        /// </summary>
        public bool NeedCreateNewGameObject => _unusedGameObjectStack.Count <= 0 && _usedGameObjectList.Count < _maxNum;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootNode">根节点</param>
        /// <param name="resourceName">路径</param>
        public ProGameObjectPool(GameObject rootNode, string resourceName)
        {
            if (ProGameObjectPoolManager.isOpenGameObjectPoolLayoutOptimization)
            {
                // 如果开启了游戏对象池布局优化，则创建节点
                _gameObjectPoolNode = new GameObject($"[{resourceName}]Node");
                _gameObjectPoolNode.transform.SetParent(rootNode.transform);
            }

            ProAbstractGameObjectPoolScriptableObject gameObjectPoolScriptableObject = null;

            
            ProAssetManager.Instance.LoadResource<ProAbstractGameObjectPoolScriptableObject>(ProConst.ScriptableObjects, resourceName, (resource =>
            {
                // 如果对象池中不存在该路径的对象，或者需要创建新的对象
                gameObjectPoolScriptableObject = resource;
                
                if (gameObjectPoolScriptableObject == null)
                {
                    // 如果未设置预制体的ScriptableObject，则使用默认值
                    _maxNum = ProConst.GameObjectPoolDefaultMaxNum;
                    ProLog.LogWarning("未设置预制体的ScriptableObject，使用默认最大值");
                }
                else
                {
                    _maxNum = gameObjectPoolScriptableObject.MaxNum;
                }
            }));
            

        }

        /// <summary>
        /// 获取游戏对象
        /// </summary>
        /// <returns>游戏对象</returns>
        public GameObject Get()
        {
            GameObject gameObject;

            if (UnusedCount > 0)
            {
                // 如果有未使用的对象，则从栈中取出
                gameObject = _unusedGameObjectStack.Pop();
                _usedGameObjectList.Add(gameObject);
            }
            else
            {
                // 如果没有未使用的对象，则从已使用的对象列表中取出第一个
                gameObject = _usedGameObjectList[0];
                _usedGameObjectList.RemoveAt(0);
                _usedGameObjectList.Add(gameObject);
            }

            gameObject.SetActive(true);

            if (ProGameObjectPoolManager.isOpenGameObjectPoolLayoutOptimization)
            {
                // 如果开启了游戏对象池布局优化，则将对象从节点中解除父子关系
                gameObject.transform.SetParent(null);
            }

            return gameObject;
        }

        /// <summary>
        /// 将游戏对象放回对象池
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        public void Push(GameObject gameObject)
        {
            gameObject.SetActive(false);

            if (ProGameObjectPoolManager.isOpenGameObjectPoolLayoutOptimization)
            {
                // 如果开启了游戏对象池布局优化，则将对象设置为节点的子对象
                gameObject.transform.SetParent(_gameObjectPoolNode.transform);
            }

            _unusedGameObjectStack.Push(gameObject);
            _usedGameObjectList.Remove(gameObject);
        }

        /// <summary>
        /// 将游戏对象放回已使用列表
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        public void PushGameObjectToUsedList(GameObject gameObject)
        {
            _usedGameObjectList.Add(gameObject);
        }
    }
}
