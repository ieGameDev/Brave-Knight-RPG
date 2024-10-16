using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private EnemyMoveToPlayer _moveToPlayer;

        private void Start()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;
            
            _moveToPlayer.enabled = false;
        }

        private void OnDestroy()
        {
            _trigger.TriggerEnter -= TriggerEnter;
            _trigger.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj) => 
            _moveToPlayer.enabled = true;

        private void TriggerExit(Collider obj) => 
            _moveToPlayer.enabled = false;
    }
}