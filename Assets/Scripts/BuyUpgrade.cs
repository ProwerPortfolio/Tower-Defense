// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Покупка апгрейда.
    /// </summary>
    public class BuyUpgrade : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private UpgradeAsset asset;

        [SerializeField] private Image upgradeIcon;

        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private TextMeshProUGUI priceText;

        [SerializeField] private Button buyButton;

        private int price;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;

            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            levelText.text = (savedLevel + 1).ToString();

            if (savedLevel >= asset.costByLevel.Length)
            {
                buyButton.gameObject.SetActive(false);
                priceText.text = "МАКСИМУМ";
                price = int.MaxValue;
            }
            else
            {
                price = asset.costByLevel[savedLevel];
                priceText.text = $"{price} звезд(-ы)";
            }
        }

        public void CheckPrice(int money)
        {
            buyButton.interactable = money >= price;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

        #endregion

        #endregion
    }
}