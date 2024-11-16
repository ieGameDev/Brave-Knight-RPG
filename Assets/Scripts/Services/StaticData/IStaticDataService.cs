using Infrastructure.DI;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        void LoadPlayer();
        EnemyData DataForEnemy(MonsterTypeId typeId);
        PlayerData DataForPlayer();
        LevelStaticData DataForLevel(string sceneKey);
    }
}