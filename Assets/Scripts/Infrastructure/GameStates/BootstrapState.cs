using Infrastructure.GameBootstrap;
using Services.Input;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            
            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.LoadScene(InitialScene, EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("TestLevel");

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