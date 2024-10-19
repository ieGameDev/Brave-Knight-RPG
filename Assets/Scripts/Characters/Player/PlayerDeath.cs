using System;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private GameObject _deathFx;

        private bool _isDead;

        public event Action OnDeath;

        private void Start() =>
            _playerHealth.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            _playerHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _playerHealth.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;

            _playerMove.enabled = false;
            _playerAnimator.PlayDeathAnimation();

            Instantiate(_deathFx, transform.position + Vector3.up * 1f, Quaternion.identity);

            OnDeath?.Invoke();
        }
    }
}