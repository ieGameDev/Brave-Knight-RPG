using CameraLogic;
using Characters.Player;
using Services.Factory;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
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
            InitSpawners();
            GameObject cameraContainer = InitialCameraContainer();
            GameObject player = InitialPlayer();
            InitialHUD(player);
            CameraFollow(cameraContainer, player);
        }

        private GameObject InitialCameraContainer() =>
            _gameFactory.CreateCameraContainer();

        private GameObject InitialPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(Constants.PlayerInitialPointTag));

        private void InitialHUD(GameObject player)
        {
            GameObject hud = _gameFactory.CreatePlayerHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<PlayerHealth>());
            hud.GetComponentInChildren<AttackButton>().Construct(player.GetComponent<PlayerAttack>());
        }

        private void CameraFollow(GameObject cameraContainer, GameObject player) =>
            cameraContainer.GetComponent<CameraFollow>().Follow(player);

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.DataForLevel(sceneKey);

            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                _gameFactory.CreateEnemySpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
    }
}