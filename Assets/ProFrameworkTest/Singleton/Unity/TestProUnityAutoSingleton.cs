using ProFramework;

namespace ProFrameworkTest
{
    public class TestProUnityAutoSingleton : ProUnityAutoSingleton<TestProUnityAutoSingleton>
    {
        private string _name;

        protected override void Awake()
        {
            base.Awake();
            _name = nameof(TestProCSharpSingleton);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"MonoBehaviour自动单例基类测试方法 类名是: {_name}");
        }
    }
}