using CameraLogic;
using Characters.Enemy;
using Characters.Player;
using Services.Factory;
using Services.Progress;
using UI;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemyInitialPointTag = "EnemyInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory, IProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject cameraContainer = InitialCameraContainer();
            GameObject player = InitialPlayer();
            InitialHUD(player);
            CameraFollow(cameraContainer, player);
            InitialEnemy();
        }

        private GameObject InitialCameraContainer() =>
            _gameFactory.CreateCameraContainer();

        private GameObject InitialPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));

        private void InitialHUD(GameObject player)
        {
            GameObject hud = _gameFactory.CreatePlayerHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<PlayerHealth>());
        }

        private void InitialEnemy()
        {
            EnemyInitialPoint enemyInitialPoint =
                GameObject.FindWithTag(EnemyInitialPointTag).GetComponent<EnemyInitialPoint>();

            _gameFactory.CreateEnemy(enemyInitialPoint);
        }

        private void CameraFollow(GameObject cameraContainer, GameObject player) =>
            cameraContainer.GetComponent<CameraFollow>().Follow(player);

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
    }
}