using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _offSetY;
        [SerializeField] private float _rotationAngleX;

        private Transform _target;
        
        private void LateUpdate()
        {
            if (!_target)
                return;

            CameraFollowing();
        }
        
        public void Follow(GameObject target) => 
            _target = target.transform;

        private void CameraFollowing()
        {
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            transform.position = position;
            transform.rotation = rotation;
        }
        
        private Vector3 FollowingPointPosition()
        {
            Vector3 targetPosition = _target.position;
            targetPosition.y += _offSetY;
            return targetPosition;
        }
    }
}