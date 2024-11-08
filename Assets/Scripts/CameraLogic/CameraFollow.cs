using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _offSetY;
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _smoothTime;

        private Transform _target;
        private Vector3 _currentVelocity;
        
        private void LateUpdate()
        {
            if (!_target) return;
            
            CameraFollowing();
        }
        
        public void Follow(GameObject target) => 
            _target = target.transform;

        private void CameraFollowing()
        {
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();
            
            transform.position =
                Vector3.SmoothDamp(transform.position, position, ref _currentVelocity, _smoothTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _smoothTime);
        }
        
        private Vector3 FollowingPointPosition()
        {
            Vector3 targetPosition = _target.position;
            targetPosition.y += _offSetY;
            return targetPosition;
        }
    }
}