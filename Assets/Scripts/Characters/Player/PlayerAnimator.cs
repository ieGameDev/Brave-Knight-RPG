using System;
using Logic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack01 = Animator.StringToHash("Attack01");

        private void Update() =>
            PlayRunAnimation();

        public void PlayHitAnimation() =>
            _animator.SetTrigger(Hit);

        public void PlayDeathAnimation() =>
            _animator.SetTrigger(Die);

        public void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack01);

        private void PlayRunAnimation() =>
            _animator.SetFloat(Run, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}