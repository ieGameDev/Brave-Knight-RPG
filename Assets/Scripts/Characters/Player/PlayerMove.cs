using Data;
using Extensions;
using Services.Input;
using Services.Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;

        private GameObject _camera;
        private IInputService _inputService;
        private PlayerStats _playerStats;

        public void Construct(GameObject followCamera, IInputService inputService)
        {
            _camera = followCamera;
            _inputService = inputService;
        }

        private void Update() =>
            PlayerMovement();

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.VectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            _playerStats = progress.PlayerStats;
            
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;
            
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            
            if (savedPosition != null)
                TransferPlayer(position: savedPosition);
        }

        private void TransferPlayer(Vector3Data position)
        {
            _characterController.enabled = false;
            transform.position = position.UnityVector();
            _characterController.enabled = true;
        }

        private void PlayerMovement()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (_playerStats.MoveSpeed * Time.deltaTime));
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}