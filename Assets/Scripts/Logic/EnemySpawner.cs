using Characters.Enemy;
using Data;
using Infrastructure.DI;
using Services.Factory;
using Services.Progress;
using Services.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        [SerializeField] private EnemyPatrolPoints _patrolPoints;

        private string _id;
        private bool _isSlain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = DiContainer.Instance.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _isSlain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isSlain)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        private GameObject Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(_monsterTypeId, transform, _patrolPoints);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.OnEnemyDeath += Slay;

            return enemy;
        }

        private void Slay()
        {
            _enemyDeath.OnEnemyDeath -= Slay;
            _isSlain = true;
        }
    }
}