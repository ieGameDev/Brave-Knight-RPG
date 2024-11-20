using System;
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
        
        protected override void Awake() => 
            _enemyHealth.HealthChanged += OnAttackEnded;

        private void Start() => 
            _fireballPool = new PoolBase<EnemyFireball>(PreloadFireball, GetAction, ReturnAction, _poolSize);

        protected override void Update()
        {
            base.Update();
            
            if(_attackIsActive)
                transform.LookAt(_player.transform);
        }

        public override void OnAttack()
        {
            EnemyFireball fireball = _fireballPool.Get();
            fireball.transform.position = _firePoint.position;
            fireball.transform.rotation = _firePoint.rotation;
            fireball.Initialize(_fireballPool, _firePoint.forward, _fireballSpeed, _damage);
        }
        
        private EnemyFireball PreloadFireball() => 
            _gameFactory.CreateFireball();

        private void GetAction(EnemyFireball fireball) => 
            fireball.gameObject.SetActive(true);

        private void ReturnAction(EnemyFireball fireball) => 
            fireball.gameObject.SetActive(false);
    }
}