using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] private Animator _animator;

        public void Move() =>
            _animator.SetBool(IsMoving, true);

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack);
    }
}