using UnityEngine;

namespace Characters.Blacksmith
{
    public class Blacksmith : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public void AnvilVFX() => _particleSystem.Play();
    }
}