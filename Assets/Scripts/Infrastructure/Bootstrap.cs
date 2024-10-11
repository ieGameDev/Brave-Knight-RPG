using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        public static IInputService Input;

        private void Awake()
        {
            InitialInputService();

            DontDestroyOnLoad(this);
        }

        private static void InitialInputService()
        {
            if (Application.isEditor)
                Input = new MobileInput();
            else
                Input = new DesktopInput();
        }
    }
}