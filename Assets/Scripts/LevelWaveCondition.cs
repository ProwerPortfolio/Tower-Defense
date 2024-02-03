// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Условие победы по результатам убитых врагов в волнах.
    /// </summary>
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        #region Parameters

        private bool isCompleted;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                isCompleted = true;
            };
        }

        #endregion

        #region Public API

        public bool IsCompleted => isCompleted;

        #endregion

        #endregion
    }
}