// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System;
using System.Collections.Generic;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ����� ������.
    /// </summary>
    public class EnemyWave : MonoBehaviour
    {
        /// <summary>
        /// �����.
        /// </summary>
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        /// <summary>
        /// ������ �������.
        /// </summary>
        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        #region Parameters

        /// <summary>
        /// �������, ������� ���������� � ������ ���������� �����.
        /// </summary>
        public static Action<float> OnWavePrepare;

        /// <summary>
        /// ����� �� ���������� �����.
        /// </summary>
        [SerializeField] private float prepareTime;

        private event Action OnWaweReady;

        [SerializeField] private PathGroup[] groups;

        /// <summary>
        /// ��������� �����.
        /// </summary>
        [SerializeField] private EnemyWave nextWave;

        #endregion

        #region API



        #region Unity API

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                OnWaweReady?.Invoke();
                enabled = false;
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// �������� ���������� �����.
        /// </summary>
        /// <returns>���������� �����.</returns>
        public float GetRemainingTime()
        {
            return prepareTime - Time.time;
        }

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaweReady += spawnEnemies;
        }

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaweReady -= spawnEnemies;
            if (nextWave)
                nextWave.Prepare(spawnEnemies);
            return nextWave;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }

        #endregion

        #endregion
    }
}