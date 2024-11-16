using System;
using Logic;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;

        public event Action HealthChanged;

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth -= damage;
            _enemyAnimator.PlayHitAnimation();

            HealthChanged?.Invoke();
        }
    }
}