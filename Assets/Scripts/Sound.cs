// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Список звуков.
    /// </summary>
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDead = 2,
        EnemyWin = 3,
        PlayerWin = 4,
        PlayerLose = 5
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}