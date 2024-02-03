// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Отвечает за отрисовку уровней.
    /// </summary>
    public class LevelDisplayController : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchLevels;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}";
            }

            for (int i = 0; i < branchLevels.Length; i++)
            {
                branchLevels[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}";
            }

            var drawLevel = 0;
            var score = 1;

            while (score != 0 && drawLevel < levels.Length)
            {
                score = levels[drawLevel].Initialize();
                drawLevel++;
            }

            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < branchLevels.Length; i++)
            {
                branchLevels[i].TryActivate();
            }
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}