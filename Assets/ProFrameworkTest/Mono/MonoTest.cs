using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class MonoTest : MonoBehaviour
    {
        private void Start()
        {
            // 添加 Update 监听函数
            ProMonoManager.Instance.AddUpdateListener(OnUpdate);
            ProMonoManager.Instance.AddFixedUpdateListener(OnFixedUpdate);
            ProMonoManager.Instance.AddLateUpdateListener(OnLateUpdate);
        }

        private void OnDestroy()
        {
            // 移除 Update 监听函数，确保不会出现内存泄漏
            ProMonoManager.Instance.RemoveUpdateListener(OnUpdate);
            ProMonoManager.Instance.RemoveFixedUpdateListener(OnFixedUpdate);
            ProMonoManager.Instance.RemoveLateUpdateListener(OnLateUpdate);
        }

        private void OnUpdate()
        {
            // 在每帧更新时执行的逻辑
            ProLog.LogDebug("每帧更新");
        }

        private void OnFixedUpdate()
        {
            // 在每个 FixedUpdate 更新时执行的逻辑
            ProLog.LogDebug("固定帧更新");
        }

        private void OnLateUpdate()
        {
            // 在每个 LateUpdate 更新时执行的逻辑
            ProLog.LogDebug("晚期更新");
        }
    }
}