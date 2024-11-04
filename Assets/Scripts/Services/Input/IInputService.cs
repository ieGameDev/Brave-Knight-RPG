using Infrastructure.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
    }
}