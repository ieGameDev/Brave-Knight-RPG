using System.Collections.Generic;
using Characters.Enemy;
using Characters.Player;
using Data;
using Infrastructure.DI;
using Logic;
using Services.AssetsManager;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using UI;
using UnityEngine;

namespace Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IInputService _input;
        private readonly IProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Player { get; set; }
        public GameObject CameraContainer { get; set; }

        public GameFactory(IAssetsProvider assetProvider, IStaticDataService staticData, IInputService input,
            IProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _input = input;
            _progressService = progressService;
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

            Camera mainCamera = Camera.main;
            PlayerMove playerMove = Player.GetComponent<PlayerMove>();
            PlayerAttack playerAttack = playerMove.GetComponent<PlayerAttack>();
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();

            playerMove.Construct(CameraContainer, _input);
            playerAttack.Construct(mainCamera);
            playerHealth.Construct(mainCamera);

            RegisterProgressWatchers(Player);

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