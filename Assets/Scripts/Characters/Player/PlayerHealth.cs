using System;
using Data;
using Services.Progress;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerAnimator _animator;

        private PlayerStats _playerStats;

        public event Action HealthChanged;

        public float Current
        {
            get => _playerStats.CurrentHP;
            set
            {
                if (_playerStats.CurrentHP != value)
                {
                    _playerStats.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _playerStats.MaxHP;
            set => _playerStats.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerStats = progress.PlayerStats;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerStats.CurrentHP = Current;
            progress.PlayerStats.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHitAnimation();
        }
    }
}