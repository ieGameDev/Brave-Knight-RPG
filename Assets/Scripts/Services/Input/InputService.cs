using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonDown() =>
            AttackButton.FireAxis;

        protected static Vector2 SimpleInputAxis() =>
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}