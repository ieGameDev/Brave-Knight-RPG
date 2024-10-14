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

        private Camera _camera;
        private IInputService _inputService;
        private float _movementSpeed;

        public void Construct(Camera mainCamera, IInputService inputService, float movementSpeed)
        {
            _camera = mainCamera;
            _inputService = inputService;
            _movementSpeed = movementSpeed;
        }

        private void Update() =>
            PlayerMovement();

        public void SaveProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.VectorData());

        public void LoadProgress(PlayerProgress progress)
        {
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

            _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}