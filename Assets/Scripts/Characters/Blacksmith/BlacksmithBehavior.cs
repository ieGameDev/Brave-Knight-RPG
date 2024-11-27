using UnityEngine;

namespace Characters.Blacksmith
{
    public class BlacksmithBehavior : MonoBehaviour
    {
        private static readonly int PlayerIsClose = Animator.StringToHash("PlayerIsClose");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnTriggerEnter(Collider other)
        {
            _animator.SetBool(PlayerIsClose, true);
        }

        private void OnTriggerExit(Collider other)
        {
            _animator.SetBool(PlayerIsClose, false);
        }

        public void AnvilFX()
        {
            _particleSystem.Play();
        }
    }
}