using System.Collections.Generic;
using Infrastructure.DI;
using Services.Progress;
using UnityEngine;

namespace Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        
        GameObject CreatePlayer(GameObject initialPoint);
        void CreatePlayerHUD();

        void CleanUp();
    }
}