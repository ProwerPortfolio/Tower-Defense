// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Путь, по которому идут враги к лагерю игрока.
    /// </summary>
    public class Path : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Зона старта появления врагов.
        /// </summary>
        [SerializeField] private CircleArea startArea;

        /// <summary>
        /// Массив точек для перемещения по ним врагами.
        /// </summary>
        [SerializeField] private AIPointPatrol[] points;

        #endregion

        #region API



        #region Unity API

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            if (points == null) return;

            foreach (var point in points)
                Gizmos.DrawWireSphere(point.transform.position, point.Radius);

            for (int i = 0; i < points.Length; i++)
            {
                if (i < points.Length - 1)
                    Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
            }
        }
#endif

        #endregion

        #region Public API

        public CircleArea StartArea => startArea;

        /// <summary>
        /// Кочичество точек перемещения.
        /// </summary>
        public int Lenght
        {
            get
            {
                return points.Length;
            }
        }

        /// <summary>
        /// Получает точку перемещения по индексу.
        /// </summary>
        /// <param name="i">Индекс точки перемещения.</param>
        /// <returns>Точка перемещения.</returns>
        public AIPointPatrol this[int i] { get => points[i]; }
        #endregion

        #endregion
    }
}