using System.Linq;
using Characters.Player;
using Logic;
using Logic.ObjectPool;
using Services.AssetsManager;
using UnityEngine;
using Utils;

namespace Characters.Enemy.Attack
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected EnemyAnimator _enemyAnimator;
        [SerializeField] protected EnemyHealth _enemyHealth;

        protected float _attackCooldown;
        protected float _effectiveDistance;
        protected float _damage;
        protected float _cleavage;
        protected float _fireballSpeed;

        protected IAssetsProvider _assetProvider;
        protected GameObject _player;
        protected PlayerDeath _playerDeath;

        protected Collider[] _hits = new Collider[1];
        protected float _cooldown;
        protected int _layerMask;
        protected bool _isAttacking;
        protected bool _attackIsActive;
        protected bool _playerIsDead;

        public void Construct(IAssetsProvider assetProvider, GameObject player, PlayerDeath playerDeath,
            float attackCooldown, float damage, float effectiveDistance, float cleavage, float fireballSpeed)
        {
            _assetProvider = assetProvider;
            _player = player;
            _playerDeath = playerDeath;
            _attackCooldown = attackCooldown;
            _damage = damage;
            _cleavage = cleavage;
            _effectiveDistance = effectiveDistance;
            _fireballSpeed = fireballSpeed;

            _playerDeath.OnPlayerDeath += StopAttack;
        }

        protected virtual void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(Constants.PlayerLayer);
            _enemyHealth.HealthChanged += OnAttackEnded;
        }

        protected virtual void Update()
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;

            if (CanAttack())
                StartAttack();
        }

        public virtual void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
                HitVFX(hit.transform.position + Vector3.up * 1.2f);
            }
        }

        public void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        protected void OnDestroy()
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

        protected void StartAttack()
        {
            transform.LookAt(_player.transform);
            _enemyAnimator.PlayAttackAnimation();

            _isAttacking = true;
        }

        protected void StopAttack() =>
            _playerIsDead = true;

        protected bool CanAttack() =>
            !_playerIsDead && _attackIsActive && !_isAttacking && _cooldown <= 0;

        private void HitVFX(Vector3 position) =>
            Instantiate(Resources.Load<GameObject>("FX/PlayerHitFX"), position, Quaternion.identity);
    }
}