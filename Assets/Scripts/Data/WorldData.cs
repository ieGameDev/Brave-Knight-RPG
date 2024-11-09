using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;

        public WorldData(string level)
        {
            PositionOnLevel = new PositionOnLevel(level);
            LootData = new LootData();
        }
    }
}