// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using TMPro;
using UnityEngine.UI;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Загружает уровень, по которому происходит клик.
    /// </summary>
    public class MapLevel : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private Episode episode;
        [SerializeField] private Image scoreBar;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public bool IsComplete => gameObject.activeSelf && scoreBar.fillAmount > 0f;

        public int Initialize()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(episode);

            if (score == 0)
                scoreBar.fillAmount = 0;

            if (score == 1)
                scoreBar.fillAmount = 0.38f;

            if (score == 2)
                scoreBar.fillAmount = 0.63f;

            if (score == 3)
                scoreBar.fillAmount = 1;

            return score;
        }

        public void LoadLevel()
        {
            if (episode)
                LevelSequenceController.Instance.StartEpisode(episode);
            else
                Debug.LogError("Назначь эпизод, друг!");
        }

        #endregion

        #endregion
    }
}