using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State PlayerState;
        public Stats PlayerStats;

        public PlayerProgress(string level)
        {
            WorldData = new WorldData(level);
            PlayerState = new State();
            PlayerStats = new Stats();
        }
    }
}