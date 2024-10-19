using System;
using Logic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack01 = Animator.StringToHash("Attack01");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _hitStateHash = Animator.StringToHash("Hit");
        private readonly int _dieStateHash = Animator.StringToHash("Die");
        private readonly int _attackStateHash = Animator.StringToHash("Attack01");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;

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

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(State);

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _runStateHash)
                state = AnimatorState.Run;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _dieStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}