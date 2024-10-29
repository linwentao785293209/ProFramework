using ProFramework;


namespace ProFrameworkTest
{
    public sealed class TestProCSharpSingletonManager : ProCSharpSingleton<TestProCSharpSingletonManager>
    {
        private string _name;

        private TestProCSharpSingletonManager()
        {
            _name = nameof(TestProCSharpSingletonManager);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"C#单例测试方法 类名是: {_name}");
        }
    }
}