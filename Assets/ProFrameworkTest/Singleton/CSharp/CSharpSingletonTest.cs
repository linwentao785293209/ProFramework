using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class CSharpSingletonTest : MonoBehaviour
    {
        void Update()
        {
            // 测试方法 正常调用
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProCSharpSingletonManager.Instance.TestMethod();
            }

            // Activator反射实例化 报错
            if (Input.GetKeyDown(KeyCode.W))
            {
                TestProCSharpSingletonManager testProCSharpSingletonManager =
                    Activator.CreateInstance(typeof(TestProCSharpSingletonManager), true) as
                        TestProCSharpSingletonManager;

                testProCSharpSingletonManager.TestMethod();
            }

            // Constructor反射实例化 报错
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

            // 抽象类尝试获取单例 报错
            if (Input.GetKeyDown(KeyCode.R))
            {
                TestAbstractProCSharpSingletonManager.Instance.TestMethod();
            }
        }
    }
}