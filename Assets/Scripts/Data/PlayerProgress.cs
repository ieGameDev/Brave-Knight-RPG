using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State PlayerState;

        public PlayerProgress(string level)
        {
            WorldData = new WorldData(level);
            PlayerState = new State();
        }
    }
}