using System.Collections.Generic;
using Characters.Enemy.EnemySpawners;
using Logic;
using Services.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect Spawners"))
            {
                EnemySpawnMarker[] spawnMarkers = FindObjectsByType<EnemySpawnMarker>(FindObjectsSortMode.None);

                levelData.EnemySpawners = new List<EnemySpawnerData>(spawnMarkers.Length);

                foreach (var spawnMarker in spawnMarkers)
                {
                    PatrolPoint patrolPoint = spawnMarker.GetComponentInChildren<PatrolPoint>();
                    Vector3 patrolPointPosition = patrolPoint != null
                        ? patrolPoint.transform.position
                        : spawnMarker.transform.position;

                    UniqueId uniqueId = spawnMarker.GetComponent<UniqueId>();

                    levelData.EnemySpawners.Add(new EnemySpawnerData
                    (
                        uniqueId.Id,
                        spawnMarker.MonsterTypeId,
                        spawnMarker.transform.position,
                        patrolPointPosition
                    ));
                }
            }

            EditorUtility.SetDirty(target);
        }
    }
}