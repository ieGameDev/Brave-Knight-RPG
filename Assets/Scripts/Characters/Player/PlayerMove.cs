using Services.Input;
using UnityEngine;

namespace Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
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
    }
}