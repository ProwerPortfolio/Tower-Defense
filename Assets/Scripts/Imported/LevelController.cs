// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using TowerDefense;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Условие для прохождения уровня.
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// Пройден ли уровень?
        /// </summary>
        bool IsCompleted { get; }
    }

    /// <summary>
    /// Контроллер уровня, проверяет все условия для его завершения.
    /// </summary>
    public class LevelController : MonoSingleton<LevelController>
    {
        #region Parameters

        /// <summary>
        /// Время прохождения в секундах, за которое будут начисляться очки.
        /// </summary>
        [SerializeField] private float referenceTime;

        /// <summary>
        /// Событие, которое вызывается при прохождении уровня.
        /// </summary>
        [SerializeField] private UnityEvent eventLevelCompleted;

        /// <summary>
        /// Массив всех интерфейсов.
        /// </summary>
        private ILevelCondition[] conditions;

        /// <summary>
        /// Уровень пройден?
        /// </summary>
        private bool isLevelCompleted;

        /// <summary>
        /// Прошедшее время с начала уровня.
        /// </summary>
        private float levelTime;

        private int levelScore = 3;

        #endregion

        #region API

        /// <summary>
        /// Проверяет: сработали ли все дочерние условия?
        /// </summary>
        private void CheckLevelConditions()
        {
            if (conditions == null || conditions.Length == 0) return;

            int completedCount = 0;

            foreach (var v in conditions)
            {
                if (v.IsCompleted)
                    completedCount++;
            }

            if (completedCount == conditions.Length)
            {
                isLevelCompleted = true;

                if (referenceTime < Time.time)
                    levelScore--;

                if (Player.Instance.isDamaged)
                    levelScore--;

                eventLevelCompleted?.Invoke();

                MapCompletion.SaveEpisodeResult(levelScore);

                LevelSequenceController.Instance?.FinishCurrentLevel(true);

                Sound.PlayerWin.Play();
            }
        }

        #region Unity API

        private void Start()
        {
            conditions = GetComponentsInChildren<ILevelCondition>();
            referenceTime += Time.time;
        }

        private void Update()
        {
            if (!isLevelCompleted)
            {
                levelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        #endregion

        #region Public API

        public float ReferenceTime => referenceTime;

        public float LevelTime => levelTime;

        #endregion

        #endregion
    }
}