// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TowerDefense;
using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Спавнер кораблей ботов.
    /// </summary>
    public class AISpawner : EntitySpawner
    {
        #region Parameters

        /// <summary>
        /// Область патрулирования.
        /// </summary>
       // [SerializeField] private AIPointPatrol patrolPoint;

        /// <summary>
        /// Путь патрулирования.
        /// </summary>
        [SerializeField] private Path patrolPath;

        /// <summary>
        /// Набор точек для патрулирования.
        /// </summary>
        [SerializeField] private Transform[] patrolControlPoints;

        /// <summary>
        /// Идентификатор команды.
        /// </summary>
        [SerializeField] private int teamId;

        /// <summary>
        /// Персональные настройки врага.
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