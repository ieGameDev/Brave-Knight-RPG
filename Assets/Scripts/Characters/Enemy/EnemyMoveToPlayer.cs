using Infrastructure.DI;
using Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _enemy;
        [SerializeField] private float _minimalDistance;

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
                _enemy.destination = _player.position;
        }

        private void InitializePLayerTransform() =>
            _player = _gameFactory.Player.transform;

        private bool StopDistanceReached() =>
            Vector3.Distance(_player.position, transform.position) >= _minimalDistance;
    }
}