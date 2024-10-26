using ProFramework;

namespace ProFrameworkTest
{
    public abstract class TestAbstractProCSharpSingleton : ProCSharpSingleton<TestAbstractProCSharpSingleton>
    {
        private string _name;

        private TestAbstractProCSharpSingleton()
        {
            _name = nameof(TestAbstractProCSharpSingleton);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"C#抽象类单例测试方法 类名是: {_name}");
        }
    }
}