using CameraLogic;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerPath = "Player/Player";
        private const string HUDPath = "Player/HUD";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
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
            GameObject initialPoint = GameObject.FindWithTag("PlayerInitialPoint");
            GameObject player = Instantiate(PlayerPath, initialPoint.transform.position);
            
            Instantiate(HUDPath);
            
            CameraFollow(player);

            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject player) => 
            Camera.main?.GetComponent<CameraFollow>().Follow(player);

        private static GameObject Instantiate(string path)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 position)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab, position, Quaternion.identity);
        }
    }
}