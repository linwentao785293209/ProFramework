using ProFramework;

namespace ProFrameworkTest
{
    public abstract class TestAbstractProCSharpSingletonManager : ProCSharpSingleton<TestAbstractProCSharpSingletonManager>
    {
        private string _name;

        private TestAbstractProCSharpSingletonManager()
        {
            _name = nameof(TestAbstractProCSharpSingletonManager);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"C#抽象类单例测试方法 类名是: {_name}");
        }
    }
}