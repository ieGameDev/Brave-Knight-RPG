namespace Infrastructure.DI
{
    public class DiContainer
    {
        private static DiContainer _instance;
        public static DiContainer Instance => _instance ??= new DiContainer();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}