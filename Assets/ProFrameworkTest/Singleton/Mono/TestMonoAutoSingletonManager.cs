using ProFramework;

namespace ProFrameworkTest
{
    public class TestMonoAutoSingletonManager : ProSingletonInMonoAuto<TestMonoAutoSingletonManager>
    {
        // 添加一个测试方法
        public void TestMethod()
        {
            ProLog.LogDebug("Mono自动单例基类测试方法");
        }
    }
}