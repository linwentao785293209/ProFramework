using ProFramework;

namespace ProFrameworkTest
{
    public abstract class FsmTestState : ProFsmAbstractState<EFsmState, EFsmTestTransition>
    {
        protected FsmTestState(EFsmState stateEnum, ProFsmAbstractStateManager<EFsmState, EFsmTestTransition> stateManager) 
            : base(stateEnum, stateManager)
        {
        }
    }
}