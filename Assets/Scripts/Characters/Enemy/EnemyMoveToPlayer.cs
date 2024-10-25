using Infrastructure.DI;
using Logic.EnemyStates;
using Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveToPlayer : MonoBehaviour, IEnemyState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _minimalDistance;
        [SerializeField] private float _moveSpeed = 4f;

        private Transform _player;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = DiContainer.Instance.Single<IGameFactory>();

            if (_gameFactory.Player != null)
                InitializePLayerTransform();
            else
                _gameFactory.PlayerCreated += InitializePLayerTransform;
        }

        private void Update()
        {
            if (_player && StopDistanceReached())
                _navMeshAgent.destination = _player.position;
        }

        public void Enter()
        {
            _navMeshAgent.speed = _moveSpeed;
            Debug.Log($"Enter MoveToPlayer State, move speed: {_navMeshAgent.speed}");
        }

        public void Exit()
        {
            Debug.Log($"Exit MoveToPlayer State");
        }

        private void InitializePLayerTransform() =>
            _player = _gameFactory.Player.transform;

        private bool StopDistanceReached() =>
            Vector3.Distance(_player.position, transform.position) >= _minimalDistance;
    }
}