using System.Collections.Generic;
using System.Linq;
using Services.AssetsManager;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, EnemyData> _enemies;
        private PlayerData _player;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyData>(AssetAddress.EnemiesDataPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public void LoadPlayer() =>
            _player = Resources.Load<PlayerData>(AssetAddress.PlayerDataPath);

        public EnemyData ForEnemy(MonsterTypeId typeId) =>
            _enemies.GetValueOrDefault(typeId);

        public PlayerData ForPlayer() =>
            _player;
    }
}