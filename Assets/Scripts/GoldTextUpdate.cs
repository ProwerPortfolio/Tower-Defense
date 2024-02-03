// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using TMPro;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Обновление UI золота.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GoldTextUpdate : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на обновляемый текст.
        /// </summary>
        private TextMeshProUGUI text;

        #endregion

        #region API

        /// <summary>
        /// Обновляет UI золота.
        /// </summary>
        /// <param name="money">Количество золота.</param>
        private void UpdateText(int money)
        {
            text.text = money.ToString();
        }

        #region Unity API

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();

            Player.Instance.GoldUpdateSubscribe(UpdateText);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}