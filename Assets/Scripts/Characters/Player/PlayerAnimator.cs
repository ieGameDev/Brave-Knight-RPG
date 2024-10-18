using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int Hit = Animator.StringToHash("Hit");

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private void Update() =>
            PlayRunAnimation();

        public void PlayHitAnimation() =>
            _animator.SetTrigger(Hit);

        private void PlayRunAnimation() =>
            _animator.SetFloat(RunHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}