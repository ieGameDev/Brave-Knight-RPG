using Infrastructure.DI;
using Services.AssetsManager;
using Services.Factory;
using Services.Input;
using Services.Progress;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly DiContainer _container;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, DiContainer container)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _container = container;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.LoadScene(InitialScene, EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _container.RegisterSingle(InitialInputService());
            _container.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _container.RegisterSingle<IGameFactory>(new GameFactory(_container.Single<IAssetsProvider>()));
            _container.RegisterSingle<IProgressService>(new ProgressService());
            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(_container.Single<IProgressService>(),
                _container.Single<IGameFactory>()));
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