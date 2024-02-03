// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using TowerDefense;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ������� ��� ����������� ������.
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// ������� �� �������?
        /// </summary>
        bool IsCompleted { get; }
    }

    /// <summary>
    /// ���������� ������, ��������� ��� ������� ��� ��� ����������.
    /// </summary>
    public class LevelController : MonoSingleton<LevelController>
    {
        #region Parameters

        /// <summary>
        /// ����� ����������� � ��������, �� ������� ����� ����������� ����.
        /// </summary>
        [SerializeField] private float referenceTime;

        /// <summary>
        /// �������, ������� ���������� ��� ����������� ������.
        /// </summary>
        [SerializeField] private UnityEvent eventLevelCompleted;

        /// <summary>
        /// ������ ���� �����������.
        /// </summary>
        private ILevelCondition[] conditions;

        /// <summary>
        /// ������� �������?
        /// </summary>
        private bool isLevelCompleted;

        /// <summary>
        /// ��������� ����� � ������ ������.
        /// </summary>
        private float levelTime;

        private int levelScore = 3;

        #endregion

        #region API

        /// <summary>
        /// ���������: ��������� �� ��� �������� �������?
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