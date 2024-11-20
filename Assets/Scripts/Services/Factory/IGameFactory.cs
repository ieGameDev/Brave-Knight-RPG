using System.Collections.Generic;
using Characters.Enemy.Attack;
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

        GameObject CreateCameraContainer();
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreatePlayerHUD();
        GameObject CreateEnemy(MonsterTypeId typeId, Transform transform, List<Vector3> patrolPoints);
        LootItem CreateLoot();
        EnemyFireball CreateFireball();

        void CreateEnemySpawner(Vector3 spawnerPosition, Vector3 patrolPoint, string spawnerId,
            MonsterTypeId monsterTypeId);

        void CleanUp();
    }
}