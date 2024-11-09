using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootHover : MonoBehaviour
    {
        [SerializeField] private float _hoverHeight = 0.3f;
        [SerializeField] private float _hoverDuration = 1f;
        private Tween _hoverTween;

        private void Start() => 
            Hover();
        
        public void StopHover() => 
            _hoverTween?.Kill();

        private void Hover()
        {
            _hoverTween = transform.DOMoveY(transform.position.y + _hoverHeight, _hoverDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}