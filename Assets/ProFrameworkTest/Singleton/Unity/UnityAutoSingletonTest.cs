using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class UnityAutoSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // ���Է��� ��������
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProUnityAutoSingletonManager.Instance.TestMethod();
            }

            // ���Զ�̬��� ���˲��GameObject��ɾ�� ����
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(TestProUnityAutoSingletonManager) + " AddComponent"
                };

                TestProUnityAutoSingletonManager testProUnityAutoSingletonManager =
                    gameObj.AddComponent<TestProUnityAutoSingletonManager>();

                testProUnityAutoSingletonManager.TestMethod();
            }
        }
    }
}