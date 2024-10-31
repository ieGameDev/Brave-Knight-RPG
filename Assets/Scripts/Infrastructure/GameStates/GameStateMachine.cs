using System;
using System.Collections.Generic;
using Infrastructure.DI;
using Services.Factory;
using Services.Progress;
using Services.StaticData;
using Utils;

namespace Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, DiContainer container)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container),

                [typeof(LoadProgressState)] =
                    new LoadProgressState(this, 
                        container.Single<IProgressService>(),
                        container.Single<ISaveLoadService>(),
                        container.Single<IStaticDataService>()
                        ),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen,
                    container.Single<IGameFactory>(), 
                    container.Single<IProgressService>()),

                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetStated<TState>();
            _activeState = state;

            return state;
        }

        private TState GetStated<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}