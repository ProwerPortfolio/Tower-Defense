// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ������� ������ � ������ �� �������.
    /// </summary>
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        #region Parameters

        /// <summary>
        /// �����, ������� ���������� ������������, ����� ��������.
        /// </summary>
        [SerializeField] private float timeLimit;

        #endregion

        #region API



        #region Unity API

        private void Awake()
        {
            timeLimit += Time.time;
        }

        #endregion

        #region Public API

        public bool IsCompleted => Time.time > timeLimit;

        #endregion

        #endregion
    }
}