using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private void Update()
        {
            if (_agent.velocity.magnitude > 1f && _agent.remainingDistance > _agent.radius)
                _enemyAnimator.Move();
            else
                _enemyAnimator.StopMoving();
        }
    }
}