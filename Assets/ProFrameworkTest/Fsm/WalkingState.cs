using ProFramework;

namespace ProFrameworkTest
{
    public class WalkingState : FsmTestState
    {
        public WalkingState(FsmTestStateManager  stateManager) : base(EFsmState.Walking, stateManager) {}

        public override void OnEnter()
        {
            ProLog.LogDebug($"进入{this.GetType()}");
        }

        public override void OnExit()
        {
            ProLog.LogDebug($"退出{this.GetType()}");
        }

        public override void OnUpdate()
        {
            // 实现进入 Idle 状态时的操作
            (stateManager as FsmTestStateManager)?.FsmTest.Walk();
        }
    }
}