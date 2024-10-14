using Data;
using Infrastructure.DI;

namespace Services.Progress
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}