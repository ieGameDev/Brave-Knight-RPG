using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class State
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() =>
            CurrentHP = MaxHP;
    }
}