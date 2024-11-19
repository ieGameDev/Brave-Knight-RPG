using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyRangedAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private EnemyHealth _enemyHealth;
    }
}