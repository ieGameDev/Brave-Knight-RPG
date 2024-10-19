using System;

namespace Data
{
    [Serializable]
    public class PlayerStats
    {
        public float CurrentHP;
        public float MaxHP;
        
        public float Damage;
        public float DamageRadius;

        public void ResetHP() =>
            CurrentHP = MaxHP;
    }
}