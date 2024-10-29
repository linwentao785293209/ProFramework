using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    [DisallowMultipleComponent]
    public sealed class TestProUnityManualSingletonManager : ProUnityManualSingleton<TestProUnityManualSingletonManager>
    {
        private string _name;

        protected override void Awake()
        {
            base.Awake();
            _name = nameof(TestProCSharpSingletonManager);
        }

        public void TestMethod()
        {
            ProLog.LogDebug($"MonoBehaviour手动单例基类测试方法 类名是: {_name}");
        }
    }
}