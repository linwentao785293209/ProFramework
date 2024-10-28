using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class MonoBehaviourTest : MonoBehaviour
    {
        private void Start()
        {
            ProMonoBehaviourManager.Instance.AddUpdateListener(OnCheckInput);
        }

        private void OnDestroy()
        {
            ProMonoBehaviourManager.Instance?.RemoveUpdateListener(OnCheckInput);
        }

        private void OnCheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProMonoBehaviourManager.Instance.AddUpdateListener(OnUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                ProMonoBehaviourManager.Instance.AddFixedUpdateListener(OnFixedUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ProMonoBehaviourManager.Instance.AddLateUpdateListener(OnLateUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ProMonoBehaviourManager.Instance.AddUpdateListener(OnUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                ProMonoBehaviourManager.Instance.AddFixedUpdateListener(OnFixedUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                ProMonoBehaviourManager.Instance.AddLateUpdateListener(OnLateUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ProMonoBehaviourManager.Instance?.RemoveUpdateListener(OnUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ProMonoBehaviourManager.Instance?.RemoveFixedUpdateListener(OnFixedUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ProMonoBehaviourManager.Instance?.RemoveLateUpdateListener(OnLateUpdate1);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ProMonoBehaviourManager.Instance?.RemoveUpdateListener(OnUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                ProMonoBehaviourManager.Instance?.RemoveFixedUpdateListener(OnFixedUpdate2);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                ProMonoBehaviourManager.Instance?.RemoveLateUpdateListener(OnLateUpdate2);
            }
        }

        private void OnUpdate1()
        {
            ProLog.LogDebug("每帧更新1");
        }

        private void OnFixedUpdate1()
        {
            ProLog.LogDebug("固定帧更新1");
        }

        private void OnLateUpdate1()
        {
            ProLog.LogDebug("晚期更新1");
        }

        private void OnUpdate2()
        {
            ProLog.LogDebug("每帧更新2");
        }

        private void OnFixedUpdate2()
        {
            ProLog.LogDebug("固定帧更新2");
        }

        private void OnLateUpdate2()
        {
            ProLog.LogDebug("晚期更新2");
        }
    }
}