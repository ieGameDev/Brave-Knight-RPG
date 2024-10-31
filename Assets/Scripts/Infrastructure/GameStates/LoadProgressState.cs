using Data;
using Services.Progress;
using Services.StaticData;
using UnityEngine;
using Utils;

namespace Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine gameStateMachine, IProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerData staticData = _staticDataService.ForPlayer();
            PlayerProgress progress = new PlayerProgress(Constants.TestLevel)
            {
                PlayerStats =
                {
                    MoveSpeed = staticData.MovementSpeed,
                    MaxHP = staticData.Health,
                    Damage = staticData.Damage,
                    DamageRadius = staticData.DamageRadius
                }
            };

            progress.PlayerStats.ResetHP();
            return progress;
        }
    }
}