using System.Linq;
using Characters.Player;
using Logic;
using Services.Factory;
using UnityEngine;
using Utils;

namespace Characters.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _cleavage;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private float _damage;

        private IGameFactory _gameFactory;
        private GameObject _player;
        private PlayerDeath _playerDeath;

        private Collider[] _hits = new Collider[1];
        private float _cooldown;
        private bool _isAttacking;
        private int _layerMask;
        private bool _attackIsActive;
        private bool _playerIsDead;

        public void Construct(GameObject player, PlayerDeath playerDeath)
        {
            _player = player;
            _playerDeath = playerDeath;
            
            _playerDeath.OnPlayerDeath += StopAttack;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(Constants.PlayerLayer);
            _enemyHealth.HealthChanged += OnAttackEnded;
        }

        private void Update()
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;

            if (CanAttack())
                StartAttack();
        }

        public void OnAttack()
        {
            if (Hit(out Collider hit))
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
        }

        public void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void OnDestroy()
        {
            _playerDeath.OnPlayerDeath -= StopAttack;
            _enemyHealth.HealthChanged -= OnAttackEnded;
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

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
            transform.LookAt(_player.transform);
            _enemyAnimator.PlayAttackAnimation();

            _isAttacking = true;
        }

        private void StopAttack() =>
            _playerIsDead = true;

        private bool CanAttack() => 
            !_playerIsDead && _attackIsActive && !_isAttacking && _cooldown <= 0;
    }
}