using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string level)
        {
            WorldData = new WorldData(level);
        }
    }
}