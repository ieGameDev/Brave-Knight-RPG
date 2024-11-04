using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Input
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Button _fireButton;
        
        private PlayerAttack _player;

        public void Construct(PlayerAttack playerAttack) => 
            _player = playerAttack;

        private void Awake() => 
            _fireButton?.onClick.AddListener(OnAttackButtonClick);

        private void OnDestroy() => 
            _fireButton?.onClick.RemoveListener(OnAttackButtonClick);

        private void OnAttackButtonClick() => 
            _player.AttackButtonClick();
    }
}