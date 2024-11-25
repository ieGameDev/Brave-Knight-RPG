using UnityEngine;

namespace Characters.Blacksmith
{
    public class BlacksmithBehavior : MonoBehaviour
    {
        private static readonly int PlayerIsClose = Animator.StringToHash("PlayerIsClose");
        [SerializeField] private Animator _animator;

        private void OnTriggerEnter(Collider other)
        {
            _animator.SetBool(PlayerIsClose, true);
        }

        private void OnTriggerExit(Collider other)
        {
            _animator.SetBool(PlayerIsClose, false);
        }
    }
}