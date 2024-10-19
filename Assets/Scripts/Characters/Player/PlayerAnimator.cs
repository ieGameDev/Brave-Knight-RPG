using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private void Update() =>
            PlayRunAnimation();

        public void PlayHitAnimation() =>
            _animator.SetTrigger(Hit);

        public void PlayDeathAnimation() =>
            _animator.SetTrigger(Die);

        private void PlayRunAnimation() =>
            _animator.SetFloat(RunHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}