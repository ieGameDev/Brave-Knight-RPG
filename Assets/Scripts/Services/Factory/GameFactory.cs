using System.Collections.Generic;
using Characters.Enemy;
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
        public GameObject CameraContainer { get; set; }

        public GameFactory(IAssetsProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreateCameraContainer()
        {
            CameraContainer = _assetProvider.Instantiate(AssetAddress.CameraContainerPath);
            return CameraContainer;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            Player = _assetProvider.Instantiate(AssetAddress.PlayerPath,
                initialPoint.transform.position + Vector3.up * 0.2f);

            RegisterProgressWatchers(Player);
            
            IInputService input = DiContainer.Instance.Single<IInputService>();

            PlayerMove playerMove = Player.GetComponent<PlayerMove>();
            PlayerAttack playerAttack = playerMove.GetComponent<PlayerAttack>();
            PlayerData playerData = Resources.Load<PlayerData>(AssetAddress.PlayerDataPath);

            float movementSpeed = playerData.MovementSpeed;

            playerMove.Construct(CameraContainer, input, movementSpeed);
            playerAttack.Construct(input);

            return Player;
        }

        public GameObject CreatePlayerHUD() =>
            _assetProvider.Instantiate(AssetAddress.HUDPath);

        public GameObject CreateEnemy(EnemyInitialPoint initialPoint)
        {
            GameObject enemy = _assetProvider.Instantiate("Enemies/Skeleton/Skeleton",
                initialPoint.transform.position + Vector3.up * 0.2f);

            PlayerDeath playerDeath = Player.GetComponent<PlayerDeath>();
            
            enemy.GetComponent<EnemyMoveToPlayer>().Construct(Player);
            enemy.GetComponent<EnemyPatrol>().Construct(initialPoint);
            enemy.GetComponent<EnemyAttack>().Construct(Player, playerDeath);
            
            return enemy;
        }

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