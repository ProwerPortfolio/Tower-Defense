// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using UnityEngine.Events;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ���������� �� ��� TowerDefense.
    /// </summary>
    public class TDPatrolController : AIController
    {
        #region Parameters

        /// <summary>
        /// ������ �� ������� ���� ������������ �����.
        /// </summary>
        private Path path;

        /// <summary>
        /// ������ ������� ����� � ���� ������������ �����.
        /// </summary>
        private int pathIndex;

        /// <summary>
        /// ������� ��� ���������� ������������ �� ���� (�� ���������� ������)
        /// </summary>
        [SerializeField] private UnityEvent onPathEnd;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// ������������� ����� ���� ��� ������������.
        /// </summary>
        /// <param name="newPath">����� ���� ��� ������������.</param>
        public void SetPath(Path newPath)
        {
            path = newPath;
            pathIndex = 0;
            SetPatrolBehavior(path[pathIndex]);
        }

        /// <summary>
        /// ������� �������� ��������� ����� � ���� �����.
        /// </summary>
        protected override void GetNewPoint()
        {
            pathIndex += 1;

            if (path.Lenght > pathIndex)
            {
                SetPatrolBehavior(path[pathIndex]);
            }
            else
            {
                onPathEnd.Invoke();
                Destroy(gameObject);
            }
        }

        #endregion

        #endregion
    }
}