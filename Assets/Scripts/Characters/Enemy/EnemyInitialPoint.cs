using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyInitialPoint : MonoBehaviour
    {
        public Transform[] PatrolWayPoints { get; private set; }
        
        private void Awake() => 
            PatrolWayPoints = GetComponentsInChildren<Transform>();
    }
}