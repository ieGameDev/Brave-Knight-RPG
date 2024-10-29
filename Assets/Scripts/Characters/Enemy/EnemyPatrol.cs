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
        
        private EnemyPatrolPoints _patrolPoints;
        private float _moveSpeed;
        private float _cooldown;
        private int _currentPointIndex;
        private bool _waiting;

        public void Construct(EnemyPatrolPoints patrolPoints, float moveSpeed, float cooldown)
        {
            _patrolPoints = patrolPoints;
            _moveSpeed = moveSpeed;
            _cooldown = cooldown;
        }

        private void Start()
        {
            _currentPointIndex = 0;
            if (_patrolPoints.PatrolWayPoints.Length > 0)
                _navMeshAgent.SetDestination(_patrolPoints.PatrolWayPoints[_currentPointIndex].position);

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
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.PatrolWayPoints.Length;
            _navMeshAgent.SetDestination(_patrolPoints.PatrolWayPoints[_currentPointIndex].position);
            _waiting = false;
        }
    }
}