using Data;
using UnityEngine;

namespace Characters.Enemy.EnemyLoot
{
    public class LootItem : MonoBehaviour
    {
        [SerializeField] private GameObject _pickUpVFX;
        [SerializeField] private LootHover _lootHover;
        [SerializeField] private float _lootMoveSpeed;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private Transform _player;

        public void Construct(WorldData worldData, Transform playerTransform)
        {
            _worldData = worldData;
            _player = playerTransform;
        }

        public void Initialize(Loot loot) =>
            _loot = loot;

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

        private void MoveToPlayer()
        {
            if (!_player)
                return;

            _lootHover.StopHover();

            Vector3 targetPosition = _player.position + Vector3.up;
            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, _lootMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                CollectLoot();
        }

        private void CollectLoot()
        {
            _worldData.LootData.Collect(_loot);

            Instantiate(_pickUpVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}