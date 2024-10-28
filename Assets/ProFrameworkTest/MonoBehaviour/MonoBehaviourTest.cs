using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class MonoBehaviourTest : MonoBehaviour
    {
        private void Start()
        {
            ProMonoBehaviourManager.Instance.AddUpdateListener(OnUpdate);
            ProMonoBehaviourManager.Instance.AddFixedUpdateListener(OnFixedUpdate);
            ProMonoBehaviourManager.Instance.AddLateUpdateListener(OnLateUpdate);
        }

        private void OnDestroy()
        {
            ProMonoBehaviourManager.Instance.RemoveUpdateListener(OnUpdate);
            ProMonoBehaviourManager.Instance.RemoveFixedUpdateListener(OnFixedUpdate);
            ProMonoBehaviourManager.Instance.RemoveLateUpdateListener(OnLateUpdate);
        }

        private void OnUpdate()
        {
            ProLog.LogDebug("每帧更新");
        }

        private void OnFixedUpdate()
        {
            ProLog.LogDebug("固定帧更新");
        }

        private void OnLateUpdate()
        {
            ProLog.LogDebug("晚期更新");
        }
    }
}