using UnityEngine;

namespace Services.Input
{
    public class DesktopInput : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                
                if(axis == Vector2.zero)
                    axis = UnityInputAxis();
                
                return axis;
            }
        }

        private static Vector2 UnityInputAxis() => 
            new(UnityEngine.Input.GetAxisRaw(Horizontal), UnityEngine.Input.GetAxisRaw(Vertical));
    }
}