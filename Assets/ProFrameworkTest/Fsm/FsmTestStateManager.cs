using ProFramework;

namespace ProFrameworkTest
{
    public class FsmTestStateManager : ProFsmAbstractStateManager<EFsmState, EFsmTestTransition>
    {
        public FsmTest FsmTest;

        public FsmTestStateManager(FsmTest fsmTest) : base()
        {
            FsmTest = fsmTest;


            // 初始化所有状态
            var idleState = new IdleState(this);
            var walkingState = new WalkingState(this);
            var runningState = new RunningState(this);

            // 添加状态到状态机
            AddState(idleState);
            AddState(walkingState);
            AddState(runningState);

            // 设置状态转换
            idleState.AddTransition(EFsmTestTransition.StartWalking, EFsmState.Walking);
            idleState.AddTransition(EFsmTestTransition.StartRunning, EFsmState.Running);

            walkingState.AddTransition(EFsmTestTransition.Stop, EFsmState.Idle);
            walkingState.AddTransition(EFsmTestTransition.StartRunning, EFsmState.Running);

            runningState.AddTransition(EFsmTestTransition.Stop, EFsmState.Idle);
        }
    }
}