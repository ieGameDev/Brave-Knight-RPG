using Data;
using Logic;
using Logic.ObjectPool;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootItem : MonoBehaviour
    {
        [SerializeField] private GameObject _pickUpVFX;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private float _lootMoveSpeed;

        private PoolBase<LootItem> _lootPool;
        private LootValue _lootValue;
        private bool _picked;
        private WorldData _worldData;
        private Transform _player;

        public void Construct(WorldData worldData, Transform playerTransform)
        {
            _worldData = worldData;
            _player = playerTransform;
        }

        public void Initialize(LootValue lootValue, PoolBase<LootItem> pool )
        {
            _lootValue = lootValue;
            _lootPool = pool;
        }

        private void Update()
        {
            if (_picked)
                MoveToPlayer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_picked)
                return;

            _picked = true;
            MoveToPlayer();
        }
        
        public void ResetState() => 
            _picked = false;

        private void MoveToPlayer()
        {
            if (!_player)
                return;

            Vector3 targetPosition = _player.position + Vector3.up;
            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, _lootMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                CollectLoot();
        }

        private void CollectLoot()
        {
            _worldData.LootData.Collect(_lootValue);

            Instantiate(_pickUpVFX, transform.position, Quaternion.identity);
            
            _trail.Clear();
            _lootPool.Return(this);
        }
    }
}