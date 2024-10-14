using Data;
using Extensions;
using Services.Factory;
using UnityEngine;

namespace Services.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IProgressService _progressService;
        private readonly IGameFactory _factory;

        public SaveLoadService(IProgressService progressService, IGameFactory factory)
        {
            _progressService = progressService;
            _factory = factory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _factory.ProgressWriters)
                progressWriter.SaveProgress(_progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}