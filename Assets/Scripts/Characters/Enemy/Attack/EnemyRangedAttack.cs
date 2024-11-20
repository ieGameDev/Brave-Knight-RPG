using Logic.ObjectPool;
using Services.AssetsManager;
using UnityEngine;

namespace Characters.Enemy.Attack
{
    public class EnemyRangedAttack : EnemyAttack
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _poolSize;
        [SerializeField] private GameObject _fireballPrefab;
        
        private PoolBase<EnemyFireball> _fireballPool;
        
        protected override void Awake()
        {
            _fireballPool = new PoolBase<EnemyFireball>(PreloadFireball, GetAction, ReturnAction, _poolSize);
            _enemyHealth.HealthChanged += OnAttackEnded;
        }

        protected override void Update()
        {
            base.Update();
            
            if(_attackIsActive)
                transform.LookAt(_player.transform);
        }

        public override void OnAttack()
        {
            EnemyFireball bullet = _fireballPool.Get();
            bullet.transform.position = _firePoint.position;
            bullet.Initialize(_fireballPool, _firePoint.forward, _fireballSpeed, _damage);
            
            Debug.Log("Shoot");
        }
        
        private EnemyFireball PreloadFireball()
        {
            GameObject bulletObject = Instantiate(_fireballPrefab);
            EnemyFireball bullet = bulletObject.GetComponent<EnemyFireball>();
            return bullet;
        }

        private void GetAction(EnemyFireball bullet) => 
            bullet.gameObject.SetActive(true);

        private void ReturnAction(EnemyFireball bullet) => 
            bullet.gameObject.SetActive(false);
    }
}