using System;
using UnityEngine;

namespace Services.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
        public Vector3 PatrolPoint;

        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position, Vector3 patrolPoint)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
            PatrolPoint = patrolPoint;
        }
    }
}