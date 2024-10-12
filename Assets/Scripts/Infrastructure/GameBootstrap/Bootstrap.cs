using Infrastructure.GameStates;
using Services.Input;
using UnityEngine;
using Utils;

namespace Infrastructure.GameBootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        public static IInputService Input;

        private void Awake()
        {
            _stateMachine = new GameStateMachine(new SceneLoader());
            _stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}