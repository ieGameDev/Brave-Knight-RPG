using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public KillData KillData;

        public PlayerProgress(string level)
        {
            WorldData = new WorldData(level);
            PlayerStats = new PlayerStats();
            KillData = new KillData();
        }
    }
}