using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects")]
    public class PlayerData : ScriptableObject
    {
        public float MovementSpeed;
    }
}