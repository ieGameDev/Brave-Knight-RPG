using UnityEngine;

namespace Services.Input
{
    public class MobileInput : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}