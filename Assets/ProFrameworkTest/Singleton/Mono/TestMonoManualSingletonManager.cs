using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    [DisallowMultipleComponent]
    public class TestMonoManualSingletonManager : ProSingletonInMonoManual<TestMonoManualSingletonManager>
    {
        // 添加一个测试方法
        public void TestMethod()
        {
            ProLog.LogDebug("Mono手动单例基类测试方法");
        }
    }
}