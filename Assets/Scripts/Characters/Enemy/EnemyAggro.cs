using System.Collections;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private EnemyMoveToPlayer _moveToPlayer;
        [SerializeField] private float _coolDown;

        private Coroutine _coroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;

            _moveToPlayer.enabled = false;
        }

        private void OnDestroy()
        {
            _trigger.TriggerEnter -= TriggerEnter;
            _trigger.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget) return;
            _hasAggroTarget = true;
                
            if (_coroutine != null)
                StopCoroutine(StopFollow());

            _moveToPlayer.enabled = true;
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;
            _hasAggroTarget = false;
                
            _coroutine = StartCoroutine(StopFollow());
        }

        private IEnumerator StopFollow()
        {
            yield return new WaitForSeconds(_coolDown);
            _moveToPlayer.enabled = false;
        }
    }
}