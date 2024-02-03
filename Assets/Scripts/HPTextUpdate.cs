// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using TMPro;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ���������� UI ��������.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HPTextUpdate : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������� �����.
        /// </summary>
        private TextMeshProUGUI text;

        #endregion

        #region API

        /// <summary>
        /// ��������� UI ��������.
        /// </summary>
        /// <param name="hp">���������� ��������.</param>
        private void UpdateText(int hp)
        {
            text.text = hp.ToString();
        }

        #region Unity API

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();

            Player.Instance.HPUpdateSubscribe(UpdateText);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}