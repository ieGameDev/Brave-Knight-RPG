using Logic.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveToPlayer : MonoBehaviour, IEnemyState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _minimalDistance;

        private float _moveSpeed;
        private Transform _player;

        public void Construct(GameObject player, float moveSpeed)
        {
            _player = player.transform;
            _moveSpeed = moveSpeed;
        }

        private void Update()
        {
            if (_player && IsPlayerOutOfReached())
                _navMeshAgent.destination = _player.position;
            else
                _navMeshAgent.ResetPath();
        }

        public void Enter() =>
            _navMeshAgent.speed = _moveSpeed;

        public void Exit()
        {
        }

        private bool IsPlayerOutOfReached() =>
            Vector3.Distance(_player.position, transform.position) >= _minimalDistance;
    }
}