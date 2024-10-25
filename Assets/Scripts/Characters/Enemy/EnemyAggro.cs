using Logic.EnemyStates;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private EnemyMoveToPlayer _moveToPlayer;
        [SerializeField] private EnemyPatrol _patrol;

        private EnemyStateMachine _stateMachine;
        private bool _hasAggroTarget;

        private void Awake() =>
            _stateMachine = new EnemyStateMachine();

        private void Start()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;

            _stateMachine.SetState(_patrol);
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
            
            _stateMachine.SetState(_moveToPlayer); 
            _moveToPlayer.enabled = true;
            _patrol.enabled = false;
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;
            _hasAggroTarget = false;

            _stateMachine.SetState(_patrol);
            _moveToPlayer.enabled = false;
            _patrol.enabled = true;
        }
    }
}