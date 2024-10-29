using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class CSharpSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // ���Է��� ��������
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProCSharpSingletonManager.Instance.TestMethod();
            }

            // Activator����ʵ���� ����
            if (Input.GetKeyDown(KeyCode.W))
            {
                TestProCSharpSingletonManager testProCSharpSingletonManager =
                    Activator.CreateInstance(typeof(TestProCSharpSingletonManager), true) as
                        TestProCSharpSingletonManager;

                testProCSharpSingletonManager.TestMethod();
            }

            // Constructor����ʵ���� ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                var type = typeof(TestProCSharpSingletonManager);

                var constructorInfo = type.GetConstructor(
                    System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Public,
                    null,
                    Type.EmptyTypes,
                    null
                );

                TestProCSharpSingletonManager testProCSharpSingletonManager =
                    (TestProCSharpSingletonManager)constructorInfo.Invoke(null);

                testProCSharpSingletonManager.TestMethod();
            }

            // �����ೢ�Ի�ȡ���� ����
            if (Input.GetKeyDown(KeyCode.R))
            {
                TestAbstractProCSharpSingletonManager.Instance.TestMethod();
            }
        }
    }
}