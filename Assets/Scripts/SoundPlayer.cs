// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Проигрыватель звуков.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoSingleton<SoundPlayer>
    {
        #region Parameters

        [SerializeField] private AudioClip[] sounds = new AudioClip[0];

        private AudioSource audioSource;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        #endregion

        #region Public API

        public void Play(Sound sound)
        {
            audioSource.PlayOneShot(sounds[(int)sound]);
        }

        #endregion

        #endregion
    }
}