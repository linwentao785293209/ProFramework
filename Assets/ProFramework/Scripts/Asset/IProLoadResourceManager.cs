using UnityEngine.Events;

namespace ProFramework
{
    public interface IProLoadResourceManager
    {
        public void LoadResource<T>(string assetBundleName, string resourceName, UnityAction<T> callBack = null,
            bool isSync = false)where T : UnityEngine.Object;
    }
}