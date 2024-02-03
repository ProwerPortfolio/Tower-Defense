// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using System;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Хранит информацию об очках прохождения и о прогрессе прохождения.
    /// </summary>
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        #region Parameters

        public const string fileName = "completion.dat";
        public const string fileName2 = "upgrades.dat";

        [SerializeField] private EpisodeScore[] completionData;
        // [SerializeField] private EpisodeScore[] branchCompletionData;

        private int totalScore;

        #endregion

        #region API

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        totalScore += levelScore - item.score;

                        item.score = levelScore;

                        Saver<EpisodeScore[]>.Save(fileName, completionData);
                    }
                }
            }
        }

        #region Unity API

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(fileName, ref completionData);

            foreach (var episodeScore in completionData)
            {
                totalScore += episodeScore.score;
            }
        }

        #endregion

        #region Public API

        public int TotalScore => totalScore;

        public static void ResetSavedData()
        {
            Saver<EpisodeScore[]>.Reset(fileName);
            Saver<EpisodeScore[]>.Reset(fileName2);
        }

        /// <summary>
        /// Сохраняет результаты пройденного эпизода на диск.
        /// </summary>
        /// <param name="levelScore">Количество очков, полученных после прохождения эпизода для их сохранения.</param>
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        public int GetEpisodeScore(Episode episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == episode)
                    return data.score;
            }

            return 0;
        }

        #endregion

        #endregion
    }
}