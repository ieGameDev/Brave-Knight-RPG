using System;

namespace Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public event Action OnChanged;

        public void Collect(LootValue lootValue)
        {
            Collected += lootValue.Value;
            OnChanged?.Invoke();
        }
    }
}