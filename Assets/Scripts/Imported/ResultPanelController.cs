// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ���������� ������ �����������.
    /// </summary>
    public class ResultPanelController : MonoSingleton<ResultPanelController>
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����� �� ��������� ������� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI kills;

        /// <summary>
        /// ������ �� ����� �� ��������� ����� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI score;

        /// <summary>
        /// ������ �� ����� �� ��������� ���������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI time;

        /// <summary>
        /// ������ �� ����� � ����������� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI result;

        /// <summary>
        /// ������ �� ����� �� ������ �����������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI buttonNextText;

        /// <summary>
        /// ������� �� ������� �������?
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
        /// ���������� ���������� ���������� ������ ������.
        /// </summary>
        /// <param name="levelResults">������ �� ���������� ������.</param>
        /// <param name="success">������� �� ������� �������?</param>
        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);

            this.success = success;

            result.text = success ? "������" : "���������";

            // kills.text = "��������: " + levelResults.killsCount.ToString();

            // score.text = "����: " + levelResults.scoreCount.ToString();

            // time.text = "�����: " + levelResults.time.ToString();

            buttonNextText.text = success ? "��������� �������" : "����������";

            Time.timeScale = 0;
        }

        /// <summary>
        /// ��������, ������������ ��� ������� �� ������ �����������.
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