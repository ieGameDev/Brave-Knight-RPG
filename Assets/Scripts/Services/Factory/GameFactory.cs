using Characters.Player;
using Infrastructure.DI;
using Services.AssetsManager;
using Services.Input;
using UnityEngine;

namespace Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetProvider;

        private GameObject _player;

        public GameFactory(IAssetsProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            _player = _assetProvider.Instantiate(AssetAddress.PlayerPath,
                initialPoint.transform.position + Vector3.up * 0.2f);
            
            Camera camera = Camera.main;
            IInputService input = DiContainer.Instance.Single<IInputService>();
            
            PlayerMove playerMove = _player.GetComponent<PlayerMove>();
            playerMove.Construct(camera, input);
            
            return _player;
        }

        public GameObject CreatePlayerHUD() => 
            _assetProvider.Instantiate(AssetAddress.HUDPath);
    }
}