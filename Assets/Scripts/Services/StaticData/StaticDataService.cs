using System.Collections.Generic;
using System.Linq;
using Services.AssetsManager;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, EnemyData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private PlayerData _player;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyData>(AssetAddress.EnemiesDataPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(AssetAddress.LevelsDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public void LoadPlayer() =>
            _player = Resources.Load<PlayerData>(AssetAddress.PlayerDataPath);

        public EnemyData DataForEnemy(MonsterTypeId typeId) =>
            _enemies.GetValueOrDefault(typeId);

        public LevelStaticData DataForLevel(string sceneKey) =>
            _levels.GetValueOrDefault(sceneKey);

        public PlayerData DataForPlayer() =>
            _player;
    }
}