using Data;
using Infrastructure.DI;

namespace Services.Progress
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}