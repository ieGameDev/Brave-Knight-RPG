using Logic;
using UnityEngine;

namespace UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            
            if(health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar() => 
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}