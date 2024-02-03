// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System;
using UnityEngine;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Хранит информацию о покупках апгрейдов.
    /// </summary>
    public class Upgrades : MonoSingleton<Upgrades>
    {
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        #region Parameters

        public const string fileName = "upgrades.dat";

        [SerializeField] private UpgradeSave[] saves;

        #endregion

        #region API



        #region Unity API

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(fileName, ref saves);
        }

        #endregion

        #region Public API

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.saves)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level += 1;
                    Saver<UpgradeSave[]>.Save(fileName, Instance.saves);
                }
            }
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.saves)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }

            return 0;
        }

        public static int GetTotalCost()
        {
            int result = 0;

            foreach (var upgade in Instance.saves)
            {
                for (int i = 0; i < upgade.level; i++)
                {
                    result += upgade.asset.costByLevel[i];
                }
            }

            return result;
        }

        #endregion

        #endregion
    }
}