using CameraLogic;
using Services.Factory;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit() => 
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            GameObject player = InitialPlayer();
            CameraFollow(player);
            InitialHUD();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private GameObject InitialPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));
        
        private void CameraFollow(GameObject player) => 
            Camera.main?.GetComponent<CameraFollow>().Follow(player);
        
        private void InitialHUD() => 
            _gameFactory.CreatePlayerHUD();
    }
}