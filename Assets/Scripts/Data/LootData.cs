using System;

namespace Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public event Action OnChanged;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            OnChanged?.Invoke();
        }
    }
}