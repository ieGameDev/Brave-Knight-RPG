using System.Collections.Generic;
using Characters.Enemy;
using Characters.Player;
using Infrastructure.DI;
using Logic;
using Services.AssetsManager;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using StaticData;
using UI;
using UnityEngine;

namespace Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Player { get; set; }
        public GameObject CameraContainer { get; set; }

        public GameFactory(IAssetsProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

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
            Camera mainCamera = Camera.main;

            PlayerMove playerMove = Player.GetComponent<PlayerMove>();
            PlayerAttack playerAttack = playerMove.GetComponent<PlayerAttack>();
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
            PlayerData playerData = Resources.Load<PlayerData>(AssetAddress.PlayerDataPath);

            float movementSpeed = playerData.MovementSpeed;

            playerMove.Construct(CameraContainer, input, movementSpeed);
            playerAttack.Construct(input, mainCamera);
            playerHealth.Construct(mainCamera);

            return Player;
        }

        public GameObject CreatePlayerHUD() =>
            _assetProvider.Instantiate(AssetAddress.HUDPath);

        public GameObject CreateEnemy(MonsterTypeId typeId, Transform transform, EnemyPatrolPoints patrolPoints)
        {
            PlayerDeath playerDeath = Player.GetComponent<PlayerDeath>();
            EnemyData enemyData = _staticData.ForEnemy(typeId);
            GameObject enemy =
                Object.Instantiate(enemyData.EnemyPrefab, transform.position, Quaternion.identity, transform);

            IHealth health = enemy.GetComponent<IHealth>();
            health.CurrentHealth = enemyData.Health;
            health.MaxHealth = enemyData.Health;
            
            float moveSpeed = enemyData.MoveSpeed;
            float patrolSpeed = enemyData.PatrolSpeed;
            float patrolCooldown = enemyData.PatrolCooldown;
            float attackCooldown = enemyData.AttackCooldown;
            float damage = enemyData.Damage;
            float effectiveDistance = enemyData.EffectiveDistance;

            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<EnemyMoveToPlayer>().Construct(Player, moveSpeed);
            enemy.GetComponent<EnemyPatrol>().Construct(patrolPoints, patrolSpeed, patrolCooldown);
            enemy.GetComponent<EnemyAttack>().Construct(Player, playerDeath, attackCooldown, damage, effectiveDistance);
            
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

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}