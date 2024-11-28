using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Blacksmith
{
    public class BlacksmithsShop : MonoBehaviour
    {
        private static readonly int PlayerIsClose = Animator.StringToHash("PlayerIsClose");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _shopWindow;
        [SerializeField] private GameObject _shopButtonCanvas;
        
        [Header("Shop Buttons")]
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            Canvas canvas = _shopWindow.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            
            _shopWindow.SetActive(false);
            _shopButtonCanvas.transform.localScale = Vector3.zero;

            _shopButton.onClick.AddListener(OnOpenShopWindow);
            _closeButton.onClick.AddListener(OnCloseShopWindow);
        }

        private void OnDestroy()
        {
            _shopButton.onClick.RemoveListener(OnOpenShopWindow);
            _closeButton.onClick.RemoveListener(OnCloseShopWindow);
        }

        private void OnOpenShopWindow()
        {
            _shopWindow.SetActive(true);
        }

        private void OnCloseShopWindow()
        {
            _shopWindow.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            _animator.SetBool(PlayerIsClose, true);
            _shopButtonCanvas.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
        }

        private void OnTriggerExit(Collider other)
        {
            _animator.SetBool(PlayerIsClose, false);
            _shopButtonCanvas.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack);
        }
    }
}