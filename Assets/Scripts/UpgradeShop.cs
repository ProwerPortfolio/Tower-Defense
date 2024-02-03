// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.UI;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Магазин апгрейдов.
    /// </summary>
    public class UpgradeShop : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private TextMeshProUGUI moneyText;

        [SerializeField] private BuyUpgrade[] sales;

        private int money;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }

            UpdateMoney();
        }

        #endregion

        #region Public API

        public void UpdateMoney()
        {
            money = MapCompletion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();
            moneyText.text = money.ToString();

            foreach (var slot in sales)
            {
                slot.CheckPrice(money);
            }
        }

        #endregion

        #endregion
    }
}