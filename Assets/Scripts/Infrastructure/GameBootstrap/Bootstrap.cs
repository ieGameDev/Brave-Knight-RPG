using Infrastructure.DI;
using Infrastructure.GameStates;
using UnityEngine;
using Utils;

namespace Infrastructure.GameBootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        private GameStateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = new GameStateMachine(new SceneLoader(), _loadingScreen, DiContainer.Instance);
            _stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}