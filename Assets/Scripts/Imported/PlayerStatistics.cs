// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ��������� ������.
    /// </summary>
    public class PlayerStatistics
    {
        #region Parameters

        /// <summary>
        /// ���������� ������� � ����������.
        /// </summary>
        public int killsCount;

        /// <summary>
        /// ���������� ����� � ����������.
        /// </summary>
        public int scoreCount;

        /// <summary>
        /// ��������� ����� ���� � ����������.
        /// </summary>
        public int time;

        #endregion

        #region API



        #region Unity API

        /// <summary>
        /// ��������� ��������.
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