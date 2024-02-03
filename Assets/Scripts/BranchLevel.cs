// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Дополенительный уровень, который открывается за звёзды.
    /// </summary>
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на корневой уровень.
        /// </summary>
        [SerializeField] private MapLevel rootLevel;

        /// <summary>
        /// Количество звёзд, требуемое для открытия уровня.
        /// </summary>
        [SerializeField] private int needStars;

        [SerializeField] private TextMeshProUGUI needStarsText;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);

            if (needStars > MapCompletion.Instance.TotalScore)
            {
                needStarsText.text = needStars.ToString();
            }
            else
            {
                needStarsText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialize();
            }
        }

        #endregion

        #endregion
    }
}