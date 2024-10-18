using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyTrigger _trigger;

        private void Start()
        {
            _trigger.TriggerEnter += OnTriggerEnter;
            _trigger.TriggerExit += OnTriggerExit;
            
            _enemyAttack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            _enemyAttack.EnableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            _enemyAttack.DisableAttack();
        }
    }
}