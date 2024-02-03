// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Подцепляемый проигрыватель звуков.
    /// </summary>
    public class SoundHook : MonoBehaviour
    {
        #region Parameters

        public Sound sound;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        public void Play()
        {
            sound.Play();
        }

        #endregion

        #endregion
    }
}