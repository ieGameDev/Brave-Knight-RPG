using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects")]
    public class PlayerData : ScriptableObject
    {
        public float MovementSpeed;
        public float Health;
        public float Damage;
    }
}