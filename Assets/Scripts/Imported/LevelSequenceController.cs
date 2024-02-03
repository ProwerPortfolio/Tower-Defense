// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Контроллер прохождения уровней.
    /// </summary>
    public class LevelSequenceController : MonoSingleton<LevelSequenceController>
    {
        #region Parameters

        /// <summary>
        /// Имя сцены главного меню.
        /// </summary>
        public static string MainMenuSceneNickname = "LevelMap";

        #endregion

        #region API

        /// <summary>
        /// Считывает результаты игрока в статистику.
        /// </summary>
        /// <param name="success">Успешно ли завершён уровень?</param>
        private void CalculateValueStatistic(bool success)
        {
            if (success)
            {
                if (LevelController.Instance.LevelTime >= 60)
                {
                    LevelStatistics.scoreCount = Player.Instance.Score;
                }
                else
                {
                    LevelStatistics.scoreCount = Player.Instance.Score * 2;
                }
            }
            else
            {
                LevelStatistics.scoreCount = Player.Instance.Score;
            }

            LevelStatistics.killsCount = Player.Instance.KillCount;

            LevelStatistics.time = (int)LevelController.Instance.LevelTime;

            GlobalPlayerStatistics.Instance.GlobalKills += LevelStatistics.killsCount;
            GlobalPlayerStatistics.Instance.GlobalScore += LevelStatistics.scoreCount;
            GlobalPlayerStatistics.Instance.GlobalTime += LevelStatistics.time;
        }

        #region Unity API



        #endregion

        #region Public API

        /// <summary>
        /// Текущий эпизод.
        /// </summary>
        public Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// Номер текущего уровня.
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// Результат предыдущего уровня: Побеждён ли он?
        /// </summary>
        public bool LastLevelResults { get; private set; }

        /// <summary>
        /// Статистика пройденного уровня.
        /// </summary>
        public PlayerStatistics LevelStatistics { get; private set; }

        /// <summary>
        /// Корабль игрока.
        /// </summary>
        public static SpaceShip PlayerShip { get; set; }

        /// <summary>
        /// Начинает эпизод.
        /// </summary>
        /// <param name="episode">Начинаемый эпизод.</param>
        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;

            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();

            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }
        
        /// <summary>
        /// Принудительный перезапуск уровня.
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Завершает текущий уровень.
        /// </summary>
        /// <param name="success">Успешно ли завершён уровень?</param>
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResults = success;

            // CalculateValueStatistic(success);

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
        }

        /// <summary>
        /// Запускает следующий уровень или выходит в главное меню, если уровней не осталось.
        /// </summary>
        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        #endregion

        #endregion
    }
}