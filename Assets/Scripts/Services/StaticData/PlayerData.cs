using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player")]
    public class PlayerData : ScriptableObject
    {
        public float MovementSpeed;
        public float Health;
        public float Damage;
    }
}