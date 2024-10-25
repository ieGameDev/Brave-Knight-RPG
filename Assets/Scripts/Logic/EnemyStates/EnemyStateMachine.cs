using UnityEngine;

namespace Logic.EnemyStates
{
    public class EnemyStateMachine
    {
        private IEnemyState _currentState;

        public void SetState(IEnemyState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}