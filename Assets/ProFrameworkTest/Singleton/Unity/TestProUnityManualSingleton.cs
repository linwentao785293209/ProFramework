using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    [DisallowMultipleComponent]
    public class TestProUnityManualSingleton : ProUnityManualSingleton<TestProUnityManualSingleton>
    {
        private string _name;

        protected override void Awake()
        {
            base.Awake();
            _name = nameof(TestProCSharpSingleton);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"MonoBehaviour手动单例基类测试方法 类名是: {_name}");
        }
    }
}