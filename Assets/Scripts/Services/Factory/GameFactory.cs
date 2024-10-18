using System;
using System.Collections.Generic;
using Characters.Player;
using Infrastructure.DI;
using ScriptableObjects;
using Services.AssetsManager;
using Services.Input;
using Services.Progress;
using UnityEngine;

namespace Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Player { get; set; }
        public event Action PlayerCreated;

        public GameFactory(IAssetsProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            Player = _assetProvider.Instantiate(AssetAddress.PlayerPath,
                initialPoint.transform.position + Vector3.up * 0.2f);
            
            RegisterProgressWatchers(Player);

            Camera camera = Camera.main;
            IInputService input = DiContainer.Instance.Single<IInputService>();

            PlayerMove playerMove = Player.GetComponent<PlayerMove>();
            PlayerData playerData = Resources.Load<PlayerData>(AssetAddress.PlayerDataPath);

            float movementSpeed = playerData.MovementSpeed;

            playerMove.Construct(camera, input, movementSpeed);

            PlayerCreated?.Invoke();
            return Player;
        }

        public GameObject CreatePlayerHUD() =>
            _assetProvider.Instantiate(AssetAddress.HUDPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressWatchers(GameObject player)
        {
            foreach (ISavedProgressReader progressReader in player.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}