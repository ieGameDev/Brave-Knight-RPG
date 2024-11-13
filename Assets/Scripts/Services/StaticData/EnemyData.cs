using UnityEngine;

namespace Services.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies")]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy Type")]
        public MonsterTypeId EnemyTypeId;
        public GameObject EnemyPrefab;
        
        [Header("Health")]
        public float Health;
        
        [Header("Moving")]
        public float MoveSpeed;
        public float PatrolSpeed;
        public float PatrolCooldown;
        
        [Header("Attack")]
        public float Damage;
        public float AttackCooldown;
        public float EffectiveDistance;
        
        [Header("Loot")]
        [Range(1,15)] public int LootValue;
        [Range(3,10)] public int LootCount;
    }
}