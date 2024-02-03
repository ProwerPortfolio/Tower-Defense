// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System;
using SpaceShooter;

#endregion

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCost = 15;
        public Sprite GUISprite;
        public Sprite sprite;
        public TurretProperties turretProperties;
        public TowerAsset[] upgradesTo;
        [SerializeField] private UpgradeAsset requiedUpgrade;
        [SerializeField] private int requiedUpgradeLevel;

        public bool IsAvialable()
        {
            if (requiedUpgrade)
            {
                return requiedUpgradeLevel <= Upgrades.GetUpgradeLevel(requiedUpgrade);
            }
            else
                return true;
        }
    }
}