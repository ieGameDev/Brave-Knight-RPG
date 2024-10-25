using System.Collections;
using Logic.EnemyStates;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private EnemyMoveToPlayer _moveToPlayer;
        [SerializeField] private EnemyPatrol _patrol;
        [SerializeField] private float _coolDown;

        private EnemyStateMachine _stateMachine;
        private Coroutine _coroutine;
        private bool _hasAggroTarget;

        private void Awake() => 
            _stateMachine = new EnemyStateMachine();

        private void Start()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;

            _moveToPlayer.enabled = false;
            _patrol.enabled = true;
        }

        private void OnDestroy()
        {
            _trigger.TriggerEnter -= TriggerEnter;
            _trigger.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget) return;
            _hasAggroTarget = true;
                
            if (_coroutine != null)
                StopCoroutine(StopFollow());
            
            _moveToPlayer.enabled = true;
            _patrol.enabled = false;
            _stateMachine.SetState(_moveToPlayer);
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;
            _hasAggroTarget = false;
                
            _coroutine = StartCoroutine(StopFollow());
        }

        private IEnumerator StopFollow()
        {
            yield return new WaitForSeconds(_coolDown);
            _moveToPlayer.enabled = false;
            _patrol.enabled = true;
            _stateMachine.SetState(_patrol);
        }
    }
}