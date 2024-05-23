using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace ProFramework
{
    public class ProAssetManager : ProSingletonInSystem<ProAssetManager>
    {
        private bool isDebug = true;

        private ProAssetManager()
        {
        }

        public void LoadResource<T>(string assetBundleName, string resourceName,
            UnityAction<T> callBack = null,
            bool isSync = false, LinkedList<EProAssetLoadType> loadTypeLinkedList = null) where T : Object
        {
            // 如果加载类型列表为空，则创建默认的加载类型列表
            if (loadTypeLinkedList == null)
            {
                loadTypeLinkedList = new LinkedList<EProAssetLoadType>();
                loadTypeLinkedList.AddLast(EProAssetLoadType.AssetBundle);
                loadTypeLinkedList.AddLast(EProAssetLoadType.Resources);
            }

            LoadResourceRecursively(assetBundleName, resourceName, loadTypeLinkedList.First, callBack, isSync);
        }


        private void LoadResourceRecursively<T>(string assetBundleName, string resourceName,
            LinkedListNode<EProAssetLoadType> loadTypeNode, UnityAction<T> callBack, bool isSync) where T : Object
        {
            if (loadTypeNode == null)
            {
                // 没有更多的加载类型可以尝试了，通知加载失败
                ProLog.LogWarning($"加载{assetBundleName}包中的{resourceName}资源失败！请检查！");
                callBack?.Invoke(null);
                return;
            }

            // 尝试使用当前加载类型加载资源
            LoadResource<T>(assetBundleName, resourceName, loadTypeNode.Value, result =>
            {
                if (result == null)
                {
                    // 如果加载失败，尝试使用下一个加载类型加载资源
                    LoadResourceRecursively(assetBundleName, resourceName, loadTypeNode.Next, callBack, isSync);
                }
                else
                {
                    // 加载成功，回调加载结果
                    callBack?.Invoke(result);
                }
            }, isSync);
        }

        private void LoadResource<T>(string assetBundleName, string resourceName,
            EProAssetLoadType assetLoadType, UnityAction<T> callBack, bool isSync) where T : Object
        {
            IProLoadResourceManager loadResourceManager;

            switch (assetLoadType)
            {
                case EProAssetLoadType.AssetBundle:
                #if UNITY_EDITOR
                    if (isDebug)
                    {
                        loadResourceManager = ProEditorResourceManager.Instance;
                    }
                    else
                    {
                        loadResourceManager = ProAssetBundleManager.Instance;
                    }
                #else
                        loadResourceManager = ProAssetBundleManager.Instance;
                #endif
                    break;
                case EProAssetLoadType.Resources:
                    loadResourceManager = ProResourceManager.Instance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(assetLoadType), assetLoadType, null);
            }

            loadResourceManager.LoadResource(assetBundleName, resourceName, callBack, isSync);
        }
    }
}