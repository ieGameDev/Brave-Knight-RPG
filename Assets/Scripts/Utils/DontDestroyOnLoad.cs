using UnityEngine;

namespace Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static DontDestroyOnLoad _instance;

        private void Awake()
        {
            if (_instance != null)
                Destroy(gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}