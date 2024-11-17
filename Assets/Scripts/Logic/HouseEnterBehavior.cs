using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class HouseEnterBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject _roof;
        [SerializeField] private GameObject _poofFx;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _duration;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrength;
        [SerializeField] private int _shakeVibrato;
        [SerializeField] private List<Transform> _fxPoints;

        private Camera _mainCamera;
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = _roof.transform.position;
            _mainCamera = Camera.main;
        }

        private void OnTriggerEnter(Collider other) =>
            _roof.transform.DOMoveY(_initialPosition.y + _offsetY, _duration);

        private void OnTriggerExit(Collider other)
        {
            _roof.transform.DOMoveY(_initialPosition.y, _duration)
                .OnComplete(() =>
                {
                    _mainCamera.transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato);
                    
                    foreach (Transform point in _fxPoints)
                        Instantiate(_poofFx, point.transform.position, Quaternion.identity);
                });
        }
    }
}