using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class UnityManualSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // ���Է��� ��������
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProUnityManualSingletonManager.Instance.TestMethod();
            }

            // ���Զ�̬��� ���˲��GameObject��ɾ�� ����
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