using ProFramework;


namespace ProFrameworkTest
{
    public class TestProCSharpSingleton : ProCSharpSingleton<TestProCSharpSingleton>
    {
        private string _name;
        
        private TestProCSharpSingleton()
        {
            _name = nameof(TestProCSharpSingleton);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"C#单例测试方法 类名是: {_name}");
        }
    }
}