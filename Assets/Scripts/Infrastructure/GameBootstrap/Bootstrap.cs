using Infrastructure.GameStates;
using Services.Input;
using UnityEngine;
using Utils;

namespace Infrastructure.GameBootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        public static IInputService Input;
        
        [SerializeField] private LoadingScreen _loadingScreen;

        private GameStateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = new GameStateMachine(new SceneLoader(), _loadingScreen);
            _stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}