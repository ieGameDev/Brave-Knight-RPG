using System.Collections;
using UnityEngine;

namespace Utils
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _screen.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_screen.alpha > 0)
            {
                _screen.alpha -= 0.06f;
                yield return new WaitForSeconds(0.015f);
            }
            
            gameObject.SetActive(false);
        }
    }
}
