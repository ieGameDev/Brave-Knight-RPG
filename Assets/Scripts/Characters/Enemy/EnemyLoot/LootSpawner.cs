using Data;
using DG.Tweening;
using Services.Factory;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootValue;
        private int _lootCount;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

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
                LootItem loot = _factory.CreateLoot();
                loot.transform.position = transform.position + Vector3.up;

                LootValue item = new LootValue { Value = _lootValue };
                loot.Initialize(item);

                LootSpread(loot);
            }
        }

        private static void LootSpread(LootItem loot)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            randomDirection.y = 0;
            float distance = 5.0f;

            Vector3 targetPosition = loot.transform.position + randomDirection * distance;
            loot?.transform.DOJump(targetPosition, 3f, 1, 0.5f).SetEase(Ease.OutQuad);
        }
    }
}