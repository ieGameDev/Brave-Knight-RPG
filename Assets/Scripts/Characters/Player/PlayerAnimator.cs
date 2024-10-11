using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int RunHash = Animator.StringToHash("Run");

        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _runHash = Animator.StringToHash("Run");

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private void Update() =>
            PlayRunAnimation();

        private void PlayRunAnimation() =>
            _animator.SetFloat(RunHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}