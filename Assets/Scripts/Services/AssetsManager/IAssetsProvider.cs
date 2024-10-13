using Infrastructure.DI;
using UnityEngine;

namespace Services.AssetsManager
{
    public interface IAssetsProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 initialPoint);
    }
}