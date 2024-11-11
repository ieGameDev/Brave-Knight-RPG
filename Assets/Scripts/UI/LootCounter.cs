using System;
using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lootCount;
        private WorldData _worldData;

        private void Start() => 
            UpdateLootCount();

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.OnChanged += UpdateLootCount;
        }

        private void UpdateLootCount() => 
            _lootCount.text = $"{_worldData.LootData.Collected}";
    }
}