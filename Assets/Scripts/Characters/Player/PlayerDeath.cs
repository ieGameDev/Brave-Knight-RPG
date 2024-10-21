using System;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private GameObject _deathFx;

        private bool _isDead;

        public event Action OnPlayerDeath;

        private void Start() =>
            _playerHealth.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            _playerHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _playerHealth.CurrentHealth <= 0)
                Die();
        }

        public void DeathFx() =>
            Instantiate(_deathFx, 
                transform.position + Vector3.up * 1.3f - Vector3.forward * 1.3f, Quaternion.identity);

        private void Die()
        {
            _isDead = true;

            _playerMove.enabled = false;
            _playerAttack.enabled = false;
            _playerAnimator.PlayDeathAnimation();

            OnPlayerDeath?.Invoke();
        }
    }
}