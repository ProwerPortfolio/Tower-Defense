// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Вызывает следующую волну врагов.
    /// </summary>
    public class NextWaveGUI : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private TextMeshProUGUI bonusAmountText;

        private EnemyWaveManager manager;

        /// <summary>
        /// Отображаемое время до следующей волны.
        /// </summary>
        private float timeToNextWave;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();

            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        private void Update()
        {
            int bonus = (int)timeToNextWave;

            if (bonus < 0)
                bonus = 0;
            
            bonusAmountText.text = bonus.ToString();

            timeToNextWave -= Time.deltaTime;
        }

        #endregion

        #region Public API

        public void CallWave()
        {
            manager.ForceNextWave();
        }

        #endregion

        #endregion
    }
}