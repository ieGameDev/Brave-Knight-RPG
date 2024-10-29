using System.Collections.Generic;
using System.Linq;
using Services.StaticData;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, EnemyData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyData>("ScriptableObjects")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public EnemyData ForEnemy(MonsterTypeId typeId) =>
            _enemies.GetValueOrDefault(typeId);
    }
}