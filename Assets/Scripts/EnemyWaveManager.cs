// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using System;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// �������� ���� ������.
    /// </summary>
    public class EnemyWaveManager : MonoBehaviour
    {
        #region Parameters

        public static event Action<Enemy> OnEnemySpawn;

        [SerializeField] private Enemy enemyPrefab;

        /// <summary>
        /// ������ �����, �� ������� ����� ������������.
        /// </summary>
        [SerializeField] private Path[] paths;

        /// <summary>
        /// ������� ����� ������.
        /// </summary>
        [SerializeField] private EnemyWave currentWave;

        /// <summary>
        /// ������� ���������� ����� ������.
        /// </summary>
        private int activeEnemyCount = 0;

        public event Action OnAllWavesDead;

        #endregion

        #region API

        private void RecordEnemyDead()
        {
            if (--activeEnemyCount == 0)
            {
                ForceNextWave();
            }
        }

        /// <summary>
        /// ������ ����� ������.
        /// </summary>
        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var enemy = Instantiate(enemyPrefab, paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                        enemy.OnEnd += RecordEnemyDead;
                        enemy.Use(asset);
                        enemy.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);

                        activeEnemyCount++;

                        OnEnemySpawn?.Invoke(enemy);
                    }
                }
                else
                {
                    Debug.LogWarning($"�������� ������ � ���� � {name}");
                }
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }

        #region Unity API

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }

        #endregion

        #region Public API

        public void ForceNextWave()
        {
            if (currentWave)
            {
                Player.Instance.AddGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (activeEnemyCount == 0)
                    OnAllWavesDead?.Invoke();
            }
        }

        #endregion

        #endregion
    }
}