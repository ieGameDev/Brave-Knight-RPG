using Data;

namespace Services.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void SaveProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}