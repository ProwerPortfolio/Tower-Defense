// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Общая глобальная статистика игрока.
    /// </summary>
    public class GlobalPlayerStatistics : MonoSingleton<GlobalPlayerStatistics>
    {
        #region Parameters

        /// <summary>
        /// Общее количество убийств игрока.
        /// </summary>
        private int globalKills;

        /// <summary>
        /// Общее количество очков игрока.
        /// </summary>
        private int globalScore;

        /// <summary>
        /// Общее количество времени игрка, проведённое в уровнях.
        /// </summary>
        private int globalTime;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public int GlobalKills
        {
            get => globalKills;

            set
            {
                globalKills = value;
                PlayerPrefs.SetInt("GlobalPlayerStatistics:globalKills", globalKills);
            }
        }

        public int GlobalScore
        {
            get => globalScore;

            set
            {
                globalScore = value;
                PlayerPrefs.SetInt("GlobalPlayerStatistics:globalScore", globalScore);
            }
        }

        public int GlobalTime
        {
            get => globalTime;

            set
            {
                globalTime = value;
                PlayerPrefs.SetInt("GlobalPlayerStatistics:globalTime", globalTime);
            }
        }

        #endregion

        #endregion
    }
}