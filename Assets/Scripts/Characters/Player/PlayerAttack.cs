using Data;
using Services.Input;
using Services.Progress;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerAnimator _animator;

        private static int _layerMask;

        private IInputService _inputService;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");

        private void Update()
        {
            if(_inputService.IsAttackButtonDown() && !_animator.IsAttacking)
                _animator.PlayAttackAnimation();
        }

        public void OnAttack()
        {
            Debug.Log("Attack");
        }

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.PlayerStats;

        private int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint()+transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}