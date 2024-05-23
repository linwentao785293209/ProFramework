using UnityEngine;

namespace ProFrameworkTest
{
    public class SingletonTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestSystemSingletonManager.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                TestMonoManualSingletonManager.Instance.TestMethod();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TestMonoAutoSingletonManager.Instance.TestMethod();
            }
        }
    }
}