// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Область скрытия интерфейса постройки башен.
    /// </summary>
    public class NullBuildSite : BuildSite
    {
        #region Parameters



        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }

        #endregion

        #endregion
    }
}