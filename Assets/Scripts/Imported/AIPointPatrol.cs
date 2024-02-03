// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        #region Parameters

#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);
#endif

        [SerializeField] private float radius;

        #endregion

        #region API



        #region Unity API

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, radius);
        }
#endif

        #endregion

        #region Public API

        public float Radius => radius;

        #endregion

        #endregion
    }
}