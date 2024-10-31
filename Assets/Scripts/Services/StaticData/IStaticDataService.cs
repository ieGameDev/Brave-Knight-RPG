using Infrastructure.DI;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        void LoadPlayer();
        EnemyData ForEnemy(MonsterTypeId typeId);
        PlayerData ForPlayer();
    }
}