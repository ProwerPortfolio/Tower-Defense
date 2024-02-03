// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using TMPro;
using UnityEngine.UI;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Контроллер покупки башни.
    /// </summary>
    public class TowerBuyControl : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private TowerAsset towerAsset;

        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Button button;

        public Transform buildSite;

        #endregion

        #region API

        private void GoldStatusCheck(int gold)
        {
            if (gold >= towerAsset.goldCost != button.interactable)
            {
                button.interactable = !button.interactable;
                text.color = button.interactable ? Color.white : Color.red;
            }
        }

        #region Unity API

        private void Start()
        {
            text.text = towerAsset.goldCost.ToString();
            button.GetComponent<Image>().sprite = towerAsset.GUISprite;
            Player.Instance.GoldUpdateSubscribe(GoldStatusCheck);
        }

        #endregion

        #region Public API

        public void Buy()
        {
            Player.Instance.TryBuild(towerAsset, buildSite);
            BuildSite.HideControls();
        }

        public void SetTowerAsset(TowerAsset asset)
        {
            towerAsset = asset;
        }

        #endregion

        #endregion
    }
}