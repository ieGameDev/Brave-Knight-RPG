using System;
using System.Collections;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private EnemyMoveToPlayer _enemyMove;
        [SerializeField] private GameObject _deathFx;

        public event Action OnEnemyDeath;

        private void Start() =>
            _enemyHealth.HealthChanged += EnemyHeathChanged;

        private void OnDestroy() =>
            _enemyHealth.HealthChanged -= EnemyHeathChanged;

        private void EnemyHeathChanged()
        {
            if (_enemyHealth.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _enemyHealth.HealthChanged -= EnemyHeathChanged;

            _enemyMove.enabled = false;
            _enemyAnimator.PlayDeathAnimation();
            StartCoroutine(DeathRoutine());

            OnEnemyDeath?.Invoke();
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(1f);

            Instantiate(_deathFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}