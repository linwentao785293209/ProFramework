using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class IdleState : FsmTestState
    {
        public IdleState(ProFsmAbstractStateManager<EFsmState, EFsmTestTransition> stateManager) 
            : base(EFsmState.Idle, stateManager)
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
            (stateManager as FsmTestStateManager)?.FsmTest.Idle();
        }

        public override void CheckTransition()
        {
            // 测试状态转换
            if (Input.GetKeyDown(KeyCode.Q))
            {
                stateManager.ChangeState(EFsmTestTransition.StartWalking);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                stateManager.ChangeState(EFsmTestTransition.StartRunning);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                stateManager.ChangeState(EFsmTestTransition.Stop);
            }
        }
    }
}
