using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonDown();
    }
}