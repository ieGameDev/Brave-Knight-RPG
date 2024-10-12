using Infrastructure.GameBootstrap;
using Services.Input;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            RegisterServices();
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
        
        private void RegisterServices()
        {
            Bootstrap.Input = InitialInputService();
        }
        
        private static IInputService InitialInputService()
        {
            if (Application.isEditor)
                return new DesktopInput();
            else
                return new MobileInput();
        }
    }
}