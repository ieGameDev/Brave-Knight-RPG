using System;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _triggerCollider;
        
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            TriggerExit?.Invoke(other);
    }
}