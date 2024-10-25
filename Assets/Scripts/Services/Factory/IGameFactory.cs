using System;
using System.Collections.Generic;
using Characters.Enemy;
using Infrastructure.DI;
using Services.Progress;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject Player { get; set; }
        
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreatePlayerHUD();
        GameObject CreateEnemy(EnemyInitialPoint initialPoint);

        void CleanUp();
    }
}