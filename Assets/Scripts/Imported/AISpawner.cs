// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TowerDefense;
using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ������� �������� �����.
    /// </summary>
    public class AISpawner : EntitySpawner
    {
        #region Parameters

        /// <summary>
        /// ������� ��������������.
        /// </summary>
       // [SerializeField] private AIPointPatrol patrolPoint;

        /// <summary>
        /// ���� ��������������.
        /// </summary>
        [SerializeField] private Path patrolPath;

        /// <summary>
        /// ����� ����� ��� ��������������.
        /// </summary>
        [SerializeField] private Transform[] patrolControlPoints;

        /// <summary>
        /// ������������� �������.
        /// </summary>
        [SerializeField] private int teamId;

        /// <summary>
        /// ������������ ��������� �����.
        /// </summary>
        [SerializeField] private EnemyAsset[] enemySettings;

        [SerializeField] private Enemy enemyPrefab;

        #endregion

        #region API

        protected override void SpawnEntities()
        {
            for (int i = 0; i < spawnsCount; i++)
            {
                GameObject entity = Instantiate(enemyPrefab.gameObject);

                if (entity.GetComponent<TDPatrolController>() != null)
                {
                    TDPatrolController aI = entity.GetComponent<TDPatrolController>();

                    SpaceShip ship = aI.GetComponent<SpaceShip>();

                    Destructible destructible = ship.GetComponent<Destructible>();

                    aI.SetPath(patrolPath);

                    // aI.patrolControlPoints = patrolControlPoints;

                    ship.iD = Random.Range(0, 100000);

                    destructible.teamId = teamId;
                }

                if (entity.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.Use(enemySettings[Random.Range(0, enemySettings.Length)]);
                }

                entity.transform.position = area.GetRandomInsideZone();
            }
        }

        #region Unity API



        #endregion

        #region Public API



        #endregion

        #endregion
    }
}