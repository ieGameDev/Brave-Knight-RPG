using UnityEngine;
using DG.Tweening;

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
            _screen.DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
