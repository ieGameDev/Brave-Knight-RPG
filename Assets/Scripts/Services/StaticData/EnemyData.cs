using UnityEngine;

namespace Services.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies")]
    public class EnemyData : ScriptableObject
    {
        public MonsterTypeId EnemyTypeId;
        
        public float MoveSpeed;
        public float PatrolSpeed;
        public float Health;
        public float Damage;
        public float AttackCooldown;
        public float PatrolCooldown;
        public float EffectiveDistance;
        
        public GameObject EnemyPrefab;
    }
}