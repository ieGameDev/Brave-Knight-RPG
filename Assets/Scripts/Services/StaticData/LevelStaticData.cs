using System.Collections.Generic;
using UnityEngine;

namespace Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
    }
}