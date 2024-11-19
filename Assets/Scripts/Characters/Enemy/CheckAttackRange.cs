using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyMeleeAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyAttack")] [SerializeField] private EnemyMeleeAttack _enemyMeleeAttack;
        [SerializeField] private EnemyTrigger _trigger;

        private void Start()
        {
            _trigger.TriggerEnter += OnTriggerEnter;
            _trigger.TriggerExit += OnTriggerExit;
            
            _enemyMeleeAttack.DisableAttack();
        }

        private void OnTriggerEnter(Collider obj)
        {
            _enemyMeleeAttack.EnableAttack();
        }

        private void OnTriggerExit(Collider obj)
        {
            _enemyMeleeAttack.DisableAttack();
        }
    }
}