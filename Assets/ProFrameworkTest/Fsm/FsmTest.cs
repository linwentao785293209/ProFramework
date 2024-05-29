using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class FsmTest : MonoBehaviour
    {
        private FsmTestStateManager _stateManager;

        void Start()
        {
            // 创建并启动状态机
            _stateManager = new FsmTestStateManager(this); // 传入 FsmTest 对象的引用
            _stateManager.Start(EFsmState.Idle);
        }

        void Update()
        {
            // 更新状态机
            _stateManager.Update();
        }

        public void Idle()
        {
            // 在 FsmTest 中实现 Idle 行为
            ProLog.LogDebug("在 FsmTest 中实现 Idle 行为");
        }

        public void Walk()
        {
            // 在 FsmTest 中实现 Walk 行为
            ProLog.LogDebug("在 FsmTest 中实现 Walk 行为");
        }

        public void Run()
        {
            // 在 FsmTest 中实现 Run 行为
            ProLog.LogDebug("在 FsmTest 中实现 Run 行为");
        }
    }
}