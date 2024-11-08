using Characters.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Input
{
    public class AttackButton : MonoBehaviour, IPointerDownHandler
    {
        private PlayerAttack _player;

        public void Construct(PlayerAttack playerAttack) => 
            _player = playerAttack;

        public void OnPointerDown(PointerEventData eventData) => 
            _player.AttackButtonClick();
    }
}