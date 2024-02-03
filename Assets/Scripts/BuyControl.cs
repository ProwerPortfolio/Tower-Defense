// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System.Collections.Generic;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Контроллер покупок.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class BuyControl : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private TowerBuyControl towerBuyControlPrefab;

        private List<TowerBuyControl> activeControl;

        /// <summary>
        /// Ссылка на RectTransform объекта.
        /// </summary>
        private RectTransform rectTransform;

        #endregion

        #region API

        /// <summary>
        /// Перемещает объект на точку постройки.
        /// </summary>
        /// <param name="buildSite">Точка постройки.</param>
        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

                rectTransform.anchoredPosition = position;

                activeControl = new List<TowerBuyControl>();

                foreach (var asset in buildSite.BuildableTowers)
                {
                    if (asset.IsAvialable())
                    {
                        var newControl = Instantiate(towerBuyControlPrefab, transform);
                        activeControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }

                if (activeControl.Count > 0)
                {
                    gameObject.SetActive(true);

                    var angle = 360 / activeControl.Count;

                    for (int i = 0; i < activeControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 80);
                        activeControl[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.buildSite = buildSite.transform.root;
                    }
                } 
            }
            else
            {
                if (activeControl != null)
                {
                    foreach (var control in activeControl)
                    {
                        Destroy(control.gameObject);
                    }

                    activeControl.Clear();
                }
                
                gameObject.SetActive(false);
            }            
        }

        #region Unity API

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);

            activeControl = new List<TowerBuyControl>();
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}