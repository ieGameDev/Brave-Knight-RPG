using Data;
using DG.Tweening;
using Logic;
using Logic.ObjectPool;
using Services.Factory;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private float _spreadDistance;

        private IGameFactory _factory;
        private PoolBase<LootItem> _lootPool;
        private int _lootValue;
        private int _lootCount;

        public void Construct(IGameFactory factory, int poolSize)
        {
            _factory = factory;
            _lootPool = new PoolBase<LootItem>(PreloadLootItem, GetAction, ReturnAction, poolSize);
        }

        private void Start() =>
            _enemyDeath.OnEnemyDeath += SpawnLoot;

        public void SetLoot(int lootValue, int lootCount)
        {
            _lootValue = lootValue;
            _lootCount = lootCount;
        }

        private void SpawnLoot()
        {
            for (int i = 0; i < _lootCount; i++)
            {
                LootItem loot = _lootPool.Get();
                loot.transform.position = transform.position + Vector3.up;

                LootValue item = new LootValue { Value = _lootValue };
                loot.Initialize(item, _lootPool);

                LootSpread(loot);
            }
        }

        private void LootSpread(LootItem loot)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            randomDirection.y = 0;

            Vector3 targetPosition = loot.transform.position + randomDirection * _spreadDistance;
            loot?.transform.DOJump(targetPosition, 3f, 1, 0.5f).SetEase(Ease.OutQuad);
        }

        private LootItem PreloadLootItem() =>
            _factory.CreateLoot();

        private void GetAction(LootItem loot)
        {
            loot.gameObject.SetActive(true);
            loot.ResetState();
        }

        private void ReturnAction(LootItem loot)
        {
            loot.transform.DOKill();
            loot.gameObject.SetActive(false);
        }
    }
}