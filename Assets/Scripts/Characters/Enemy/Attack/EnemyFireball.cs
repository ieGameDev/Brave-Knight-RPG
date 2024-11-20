using System.Collections;
using Logic;
using Logic.ObjectPool;
using UnityEngine;

namespace Characters.Enemy.Attack
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyFireball : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private GameObject _fireballExplosionVFX;
        
        private PoolBase<EnemyFireball> _pool;
        private float _damage;

        public void Initialize(PoolBase<EnemyFireball> pool, Vector3 direction, float speed, float damage)
        {
            _pool = pool;
            _damage = damage;
            _rigidbody.linearVelocity = direction * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
                health.TakeDamage(_damage);

            ReturnFireball();
        }

        private IEnumerator DestroyFireball()
        {
            yield return new WaitForSeconds(2);
            ReturnFireball();
        }

        private void ReturnFireball()
        {
            _trail.Clear();
            Instantiate(_fireballExplosionVFX, transform.position, Quaternion.identity);
            _pool.Return(this);
        }
    }
}