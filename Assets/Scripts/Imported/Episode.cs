// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ������.
    /// </summary>
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        #region Parameters

        /// <summary>
        /// ��� �������.
        /// </summary>
        [SerializeField] private string episodeName;

        /// <summary>
        /// ����� �������, ������� ������������ ���������������.
        /// </summary>
        [SerializeField] private string[] levels;

        /// <summary>
        /// ��������� ������.
        /// </summary>
        [SerializeField] private Sprite previewImage;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public string EpisodeName => episodeName;

        public string[] Levels => levels;

        public Sprite PreviewImage => previewImage;

        #endregion

        #endregion
    }
}