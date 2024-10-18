using System.Linq;
using Characters.Player;
using Infrastructure.DI;
using Services.Factory;
using UnityEngine;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _cleavage;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private float _damage;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;

        private Collider[] _hits = new Collider[1];
        private float _cooldown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = DiContainer.Instance.Single<IGameFactory>();
            _gameFactory.PlayerCreated += OnPlayerCreated;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;

            if (_attackIsActive && !_isAttacking && _cooldown <= 0)
                StartAttack();
        }

        public void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
            }
        }

        public void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() => _attackIsActive = true;

        public void DisableAttack() => _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * _effectiveDistance;

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _enemyAnimator.PlayAttack();

            _isAttacking = true;
        }

        private void OnPlayerCreated() =>
            _playerTransform = _gameFactory.Player.transform;
    }
}