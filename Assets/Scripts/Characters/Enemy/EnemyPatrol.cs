using System.Collections;
using Logic.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _moveSpeed = 2f;
        
        private EnemyInitialPoint _initialPoint;
        private int _currentPointIndex;
        private bool _waiting;

        public void Construct(EnemyInitialPoint initialPoint) => 
            _initialPoint = initialPoint;

        private void Start()
        {
            _currentPointIndex = 0;
            if (_initialPoint.PatrolWayPoints.Length > 0)
                _navMeshAgent.SetDestination(_initialPoint.PatrolWayPoints[_currentPointIndex].position);

            _waiting = false;
        }

        private void Update()
        {
            if (!_waiting && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                StartCoroutine(WaitAtPoint());
        }

        public void Enter() => 
            _navMeshAgent.speed = _moveSpeed;

        public void Exit()
        {
        }

        private IEnumerator WaitAtPoint()
        {
            _waiting = true;

            yield return new WaitForSeconds(_cooldown);
            _currentPointIndex = (_currentPointIndex + 1) % _initialPoint.PatrolWayPoints.Length;
            _navMeshAgent.SetDestination(_initialPoint.PatrolWayPoints[_currentPointIndex].position);
            _waiting = false;
        }
    }
}