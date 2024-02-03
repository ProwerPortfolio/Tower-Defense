// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ����� ���������� ���������� ������.
    /// </summary>
    public class GlobalPlayerStatistics : MonoSingleton<GlobalPlayerStatistics>
    {
        #region Parameters

        /// <summary>
        /// ����� ���������� ������� ������.
        /// </summary>
        private int globalKills;

        /// <summary>
        /// ����� ���������� ����� ������.
        /// </summary>
        private int globalScore;

        /// <summary>
        /// ����� ���������� ������� �����, ���������� � �������.
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