// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Контроллер панели результатов.
    /// </summary>
    public class ResultPanelController : MonoSingleton<ResultPanelController>
    {
        #region Parameters

        /// <summary>
        /// Ссылка на текст со значением убийств игрока.
        /// </summary>
        [SerializeField] private TextMeshProUGUI kills;

        /// <summary>
        /// Ссылка на текст со значением очков игрока.
        /// </summary>
        [SerializeField] private TextMeshProUGUI score;

        /// <summary>
        /// Ссылка на текст со значением прошедшего времени.
        /// </summary>
        [SerializeField] private TextMeshProUGUI time;

        /// <summary>
        /// Ссылка на текст с результатом уровня.
        /// </summary>
        [SerializeField] private TextMeshProUGUI result;

        /// <summary>
        /// Ссылка на текст на кнопке продолжения.
        /// </summary>
        [SerializeField] private TextMeshProUGUI buttonNextText;

        /// <summary>
        /// Успешно ли пройден уровень?
        /// </summary>
        private bool success;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Отображает результаты прошедшего уровня игрока.
        /// </summary>
        /// <param name="levelResults">Ссылка на статистику игрока.</param>
        /// <param name="success">Успешно ли пройден уровень?</param>
        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            this.success = success;

            result.text = success ? "ПОБЕДА" : "ПОРАЖЕНИЕ";

            // kills.text = "Убийства: " + levelResults.killsCount.ToString();

            // score.text = "Очки: " + levelResults.scoreCount.ToString();

            // time.text = "Время: " + levelResults.time.ToString();

            buttonNextText.text = success ? "Следующий уровень" : "Перезапуск";

            Time.timeScale = 0;
        }

        /// <summary>
        /// Действие, выполняющеся при нажатии на кнопку продолжения.
        /// </summary>
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }

        #endregion

        #endregion
    }
}