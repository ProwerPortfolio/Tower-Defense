// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Статистка игрока.
    /// </summary>
    public class PlayerStatistics
    {
        #region Parameters

        /// <summary>
        /// Количество убийств в статистике.
        /// </summary>
        public int killsCount;

        /// <summary>
        /// Количество очков в статистике.
        /// </summary>
        public int scoreCount;

        /// <summary>
        /// Прошедшее время игры в статистике.
        /// </summary>
        public int time;

        #endregion

        #region API



        #region Unity API

        /// <summary>
        /// Обнуление значений.
        /// </summary>
        public void Reset()
        {
            killsCount = 0;

            scoreCount = 0;

            time = 0;
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}