// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using UnityEngine.Events;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Контроллер ИИ для TowerDefense.
    /// </summary>
    public class TDPatrolController : AIController
    {
        #region Parameters

        /// <summary>
        /// Ссылка на текущий путь передвижения врага.
        /// </summary>
        private Path path;

        /// <summary>
        /// Индекс текущей точки в пути передвижения врага.
        /// </summary>
        private int pathIndex;

        /// <summary>
        /// Событие при завершении передвижения по пути (по достижению лагеря)
        /// </summary>
        [SerializeField] private UnityEvent onPathEnd;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// Устанавливает новый путь для передвижения.
        /// </summary>
        /// <param name="newPath">Новый путь для передвижения.</param>
        public void SetPath(Path newPath)
        {
            path = newPath;
            pathIndex = 0;
            SetPatrolBehavior(path[pathIndex]);
        }

        /// <summary>
        /// Попытка получить следующую точку в пути врага.
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