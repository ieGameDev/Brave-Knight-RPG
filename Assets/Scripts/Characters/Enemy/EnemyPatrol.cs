using System.Collections;
using System.Collections.Generic;
using Logic.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyPatrol : MonoBehaviour, IEnemyState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private List<Vector3> _patrolPoints;
        private float _moveSpeed;
        private float _cooldown;
        private int _currentPointIndex;
        private bool _waiting;

        public void Construct(List<Vector3> patrolPoints, float moveSpeed, float cooldown)
        {
            _patrolPoints = patrolPoints;
            _moveSpeed = moveSpeed;
            _cooldown = cooldown;
        }

        private void Start()
        {
            _currentPointIndex = 0;
            if (_patrolPoints.Count > 0)
                _navMeshAgent.SetDestination(_patrolPoints[_currentPointIndex]);

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
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
            _navMeshAgent.SetDestination(_patrolPoints[_currentPointIndex]);
            _waiting = false;
        }
    }
}