// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// Эпизод.
    /// </summary>
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        #region Parameters

        /// <summary>
        /// Имя эпизода.
        /// </summary>
        [SerializeField] private string episodeName;

        /// <summary>
        /// Имена уровней, которые подгружаются последовательно.
        /// </summary>
        [SerializeField] private string[] levels;

        /// <summary>
        /// Превьюшка уровня.
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