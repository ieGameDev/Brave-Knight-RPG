using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class HouseEnterBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject _roof;
        [SerializeField] private GameObject _poofFx;
        [SerializeField] private List<Transform> _fxPoints;

        private Camera _mainCamera;
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = _roof.transform.position;
            _mainCamera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            _mainCamera.transform.DOShakePosition(0.2f, 0.2f, 2);
            _roof.transform.DOMoveY(_initialPosition.y + 50, 0.7f);
        }

        private void OnTriggerExit(Collider other)
        {
            _roof.transform.DOMoveY(_initialPosition.y, 0.5f)
                .OnComplete(() =>
                {
                    _mainCamera.transform.DOShakePosition(0.2f, 1, 10);
                    
                    foreach (Transform point in _fxPoints)
                        Instantiate(_poofFx, point.transform.position, Quaternion.identity);
                });
        }
    }
}