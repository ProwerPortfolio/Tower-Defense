// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Точка постройки башни.
    /// </summary>
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        #region Parameters

        public static event Action<BuildSite> OnClickEvent;

        [SerializeField] private TowerAsset[] buildableTowers;

        public TowerAsset[] BuildableTowers => buildableTowers;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }

        public void SetBuildableTowers(TowerAsset[] towers)
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                buildableTowers = towers;
            }
        }

        #endregion

        #endregion
    }
}