// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;
using System;
using UnityEngine.EventSystems;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// 
    /// </summary>
    public class ClickProtection : MonoSingleton<ClickProtection>, IPointerClickHandler
    {
        #region Parameters

        private Image blocker;

        private Action<Vector2> onClickAction;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            blocker = GetComponent<Image>();
        }

        #endregion

        #region Public API

        public void Activate(Action<Vector2> mouseAction)
        {
            blocker.enabled = true;
            onClickAction = mouseAction;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            blocker.enabled = false;
            onClickAction(eventData.pressPosition);
            onClickAction = null;
        }

        #endregion

        #endregion
    }
}