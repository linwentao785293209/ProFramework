using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class UnityManualSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // 测试方法 正常调用
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProUnityManualSingletonManager.Instance.TestMethod();
            }

            // 尝试动态添加 添加瞬间GameObject被删除 报错
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(TestProUnityManualSingletonManager) + " AddComponent"
                };

                TestProUnityManualSingletonManager testProUnityManualSingletonManager =
                    gameObj.AddComponent<TestProUnityManualSingletonManager>();

                testProUnityManualSingletonManager.TestMethod();
            }
        }
    }
}