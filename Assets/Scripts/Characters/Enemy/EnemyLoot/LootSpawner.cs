using Data;
using Services.Factory;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _factory;
        private int _lootCount;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        private void Start() => 
            _enemyDeath.OnEnemyDeath += SpawnLoot;

        private void SpawnLoot()
        {
            LootItem loot = _factory.CreateLoot();
            loot.transform.position = transform.position + Vector3.up;

            Loot item = new Loot { Value = _lootCount };

            loot.Initialize(item);
        }

        public void SetLoot(int lootCount)
        {
            _lootCount = lootCount;
        }
    }
}