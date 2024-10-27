using Data;
using Extensions;
using Services.Factory;
using UnityEngine;
using Utils;

namespace Services.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
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
                progressWriter.UpdateProgress(_progressService.Progress);
            
            PlayerPrefs.SetString(Constants.ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(Constants.ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}