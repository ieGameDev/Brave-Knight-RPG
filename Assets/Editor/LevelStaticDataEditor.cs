using System.Linq;
using Characters.Enemy.EnemySpawners;
using Logic;
using Services.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                levelData.EnemySpawners = FindObjectsByType<EnemySpawnMarker>(FindObjectsSortMode.None)
                    .Select(x =>
                    {
                        PatrolPoint patrolPoint = x.GetComponentInChildren<PatrolPoint>();
                        Vector3 patrolPointPosition =
                            patrolPoint != null ? patrolPoint.transform.position : x.transform.position;

                        return new EnemySpawnerData
                        (
                            x.GetComponent<UniqueId>().Id,
                            x.MonsterTypeId,
                            x.transform.position,
                            patrolPointPosition
                        );
                    })
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}