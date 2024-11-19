using UnityEngine;

namespace Characters.Enemy.Attack
{
    public class EnemyRangedAttack : EnemyAttack
    {
        protected override void Awake() => 
            _enemyHealth.HealthChanged += OnAttackEnded;

        public override void OnAttack()
        {
            Debug.Log("Shoot");
        }
    }
}