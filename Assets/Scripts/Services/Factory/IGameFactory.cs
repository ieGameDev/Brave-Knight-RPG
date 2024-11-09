using System.Collections.Generic;
using Characters.Enemy;
using Characters.Enemy.EnemyLoot;
using Infrastructure.DI;
using Services.Progress;
using Services.StaticData;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject Player { get; set; }
        GameObject CameraContainer { get; set; }

        GameObject CreateCameraContainer();
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreatePlayerHUD();
        GameObject CreateEnemy(MonsterTypeId typeId, Transform transform, EnemyPatrolPoints patrolPoints);
        LootItem CreateLoot();

        void CleanUp();
        void Register(ISavedProgressReader progressReader);
    }
}