using Infrastructure.DI;
using Services.AssetsManager;
using Services.Factory;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
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
            _sceneLoader.LoadScene(Constants.InitialScene, EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();

            _container.RegisterSingle(InitialInputService());
            _container.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _container.RegisterSingle<IProgressService>(new ProgressService());

            _container.RegisterSingle<IGameFactory>(new GameFactory
                (
                    _container.Single<IAssetsProvider>(),
                    _container.Single<IStaticDataService>(),
                    _container.Single<IInputService>(),
                    _container.Single<IProgressService>()
                )
            );


            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService
                (
                    _container.Single<IProgressService>(),
                    _container.Single<IGameFactory>()
                )
            );
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadEnemies();
            staticData.LoadPlayer();
            _container.RegisterSingle(staticData);
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