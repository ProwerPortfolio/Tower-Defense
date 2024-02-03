// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ���������� ����������� �������.
    /// </summary>
    public class LevelSequenceController : MonoSingleton<LevelSequenceController>
    {
        #region Parameters

        /// <summary>
        /// ��� ����� �������� ����.
        /// </summary>
        public static string MainMenuSceneNickname = "LevelMap";

        #endregion

        #region API

        /// <summary>
        /// ��������� ���������� ������ � ����������.
        /// </summary>
        /// <param name="success">������� �� �������� �������?</param>
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
        /// ������� ������.
        /// </summary>
        public Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// ����� �������� ������.
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// ��������� ����������� ������: ������� �� ��?
        /// </summary>
        public bool LastLevelResults { get; private set; }

        /// <summary>
        /// ���������� ����������� ������.
        /// </summary>
        public PlayerStatistics LevelStatistics { get; private set; }

        /// <summary>
        /// ������� ������.
        /// </summary>
        public static SpaceShip PlayerShip { get; set; }

        /// <summary>
        /// �������� ������.
        /// </summary>
        /// <param name="episode">���������� ������.</param>
        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;

            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();

            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }
        
        /// <summary>
        /// �������������� ���������� ������.
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// ��������� ������� �������.
        /// </summary>
        /// <param name="success">������� �� �������� �������?</param>
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResults = success;

            // CalculateValueStatistic(success);

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
        }

        /// <summary>
        /// ��������� ��������� ������� ��� ������� � ������� ����, ���� ������� �� ��������.
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