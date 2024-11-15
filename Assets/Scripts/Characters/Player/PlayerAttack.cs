using Data;
using DG.Tweening;
using Logic;
using Services.Progress;
using UnityEngine;
using Utils;

namespace Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerAnimator _animator;

        private static int _layerMask;

        private Camera _camera;
        private Collider[] _hits = new Collider[3];
        private PlayerStats _playerStats;

        public void Construct(Camera mainCamera) =>
            _camera = mainCamera;

        private void Awake() =>
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_playerStats.Damage);
                AttackShakeCamera();
                HitVFX(_hits[i].transform.position);
            }
        }

        public void AttackButtonClick() =>
            _animator.PlayAttackAnimation();

        public void LoadProgress(PlayerProgress progress) =>
            _playerStats = progress.PlayerStats;

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _playerStats.DamageRadius, _hits,
                _layerMask);

        private Vector3 StartPoint() =>
            new(transform.position.x, _characterController.center.y / 2, transform.position.z);

        private void AttackShakeCamera()
        {
            _camera?
                .DOShakePosition(0.12f, 0.1f, 2, 90f, true, ShakeRandomnessMode.Harmonic)
                .SetEase(Ease.InOutBounce);

            _camera?
                .DOShakeRotation(0.12f, 0.1f, 2, 90f, true, ShakeRandomnessMode.Harmonic)
                .SetEase(Ease.InOutBounce);
        }

        private void HitVFX(Vector3 position) =>
            Instantiate(Resources.Load<GameObject>("FX/EnemyHitFX"), position, Quaternion.identity);
    }
}