using Characters.Player;
using UnityEngine;

namespace UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        
        private PlayerHealth _playerHealth;

        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() => 
            _playerHealth.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
        }
    }
}