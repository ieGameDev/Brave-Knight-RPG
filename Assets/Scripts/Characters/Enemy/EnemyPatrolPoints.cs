using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyPatrolPoints : MonoBehaviour
    {
        public Transform[] PatrolWayPoints { get; private set; }
        
        private void Awake() => 
            PatrolWayPoints = GetComponentsInChildren<Transform>();
    }
}