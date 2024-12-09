using UnityEngine;

namespace UI
{
    public class JoystickCanvas : MonoBehaviour
    {
        private void Start()
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 10;
        }
    }
}