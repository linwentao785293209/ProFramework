using System;
using UnityEngine;

namespace ProFrameworkTest
{
    public class SingletonTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestProCSharpSingleton.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                TestProCSharpSingleton testProCSharpSingleton =
                    Activator.CreateInstance(typeof(TestProCSharpSingleton), true) as TestProCSharpSingleton;
                
                testProCSharpSingleton.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                var type = typeof(TestProCSharpSingleton);

                var constructorInfo = type.GetConstructor(
                    System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Public,
                    null,
                    Type.EmptyTypes,
                    null
                );

                TestProCSharpSingleton testProCSharpSingleton =
                    (TestProCSharpSingleton)constructorInfo.Invoke(null);
                
                testProCSharpSingleton.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                TestAbstractProCSharpSingleton.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                TestProUnityAutoSingleton.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(TestProUnityAutoSingleton) + " AddComponent"
                };

                TestProUnityAutoSingleton testProUnityAutoSingleton = gameObj.AddComponent<TestProUnityAutoSingleton>();

                testProUnityAutoSingleton.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                TestProUnityManualSingleton.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject gameObj = new GameObject
                {
                    name = nameof(TestProUnityManualSingleton) + " AddComponent"
                };

                TestProUnityManualSingleton testProUnityManualSingleton =
                    gameObj.AddComponent<TestProUnityManualSingleton>();

                testProUnityManualSingleton.TestMethod();
            }
        }
    }
}