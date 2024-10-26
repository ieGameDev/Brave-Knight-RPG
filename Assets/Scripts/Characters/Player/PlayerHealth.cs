using System;
using Data;
using Logic;
using Services.Progress;
using DG.Tweening;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private PlayerAnimator _animator;

        private PlayerStats _playerStats;
        private Camera _camera;

        public event Action HealthChanged;

        public float CurrentHealth
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

        public float MaxHealth
        {
            get => _playerStats.MaxHP;
            set => _playerStats.MaxHP = value;
        }

        public void Construct(Camera mainCamera) =>
            _camera = mainCamera;

        public void LoadProgress(PlayerProgress progress)
        {
            _playerStats = progress.PlayerStats;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerStats.CurrentHP = CurrentHealth;
            progress.PlayerStats.MaxHP = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth -= damage;
            _animator.PlayHitAnimation();
            HitShakeCamera();
        }

        private void HitShakeCamera() =>
            _camera?
                .DOShakePosition(0.12f, 0.3f, 3, 90f, true, ShakeRandomnessMode.Harmonic)
                .SetEase(Ease.InOutBounce);
    }
}