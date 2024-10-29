using Infrastructure.DI;
using StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyData ForEnemy(MonsterTypeId typeId);
    }
}