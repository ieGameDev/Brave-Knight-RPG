using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");

        [SerializeField] private Animator _animator;

        public void MoveAnimation(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void PlayDeathAnimation() =>
            _animator.SetTrigger(Die);

        public void PlayAttackAnimation() =>
            _animator.SetTrigger(Attack);

        public void PlayHitAnimation() =>
            _animator.SetTrigger(Hit);
    }
}