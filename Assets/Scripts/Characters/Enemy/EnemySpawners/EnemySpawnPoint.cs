using Data;
using Services.Factory;
using Services.Progress;
using Services.StaticData;
using UnityEngine;

namespace Characters.Enemy.EnemySpawners
{
    public class EnemySpawnPoint : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private EnemyPatrolPoints _patrolPoints;

        private bool _isSlain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        
        public string Id { get; set; }
        public MonsterTypeId MonsterTypeId { get; set; }

        public void Construct(IGameFactory factory) => 
            _factory = factory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _isSlain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(MonsterTypeId, transform, _patrolPoints);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.OnEnemyDeath += Slay;
        }

        private void Slay()
        {
            _enemyDeath.OnEnemyDeath -= Slay;
            _isSlain = true;
        }
    }
}