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
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _cooldown;
        [SerializeField] private Transform[] _points;

        private int _currentPointIndex;
        private bool _waiting;

        private void Start() => 
            _navMeshAgent.speed = _moveSpeed;

        private void Update()
        {
            if (!_waiting && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                StartCoroutine(WaitAtPoint());
        }

        public void Enter()
        {
            Debug.Log("PatrolState");
            
            _currentPointIndex = 0;
            if (_points.Length > 0)
                _navMeshAgent.SetDestination(_points[_currentPointIndex].position);

            _waiting = false;
        }

        public void Exit()
        {
        }

        private IEnumerator WaitAtPoint()
        {
            _waiting = true;

            yield return new WaitForSeconds(_cooldown);
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
            _navMeshAgent.SetDestination(_points[_currentPointIndex].position);
            _waiting = false;
        }
    }
}