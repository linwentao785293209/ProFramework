using ProFramework;

namespace ProFrameworkTest
{
    public class RunningState : FsmTestState
    {
        public RunningState(ProFsmAbstractStateManager<EFsmState, EFsmTestTransition> stateManager) 
            : base(EFsmState.Running, stateManager)
        {
        }

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
            (stateManager as FsmTestStateManager)?.FsmTest.Run();
        }
    }
}