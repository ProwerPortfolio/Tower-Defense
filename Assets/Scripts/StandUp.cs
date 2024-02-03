// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ‘иксирует поворот спрайта вверх.
    /// </summary>
    public class StandUp : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// —сылка на Rigidbody2D.
        /// </summary>
        private Rigidbody2D rig;

        /// <summary>
        /// —сылка на SpriteRenderer.
        /// </summary>
        private SpriteRenderer sr;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            var xMotion = rig.velocity.x;

            if (xMotion > 0.01f)
                sr.flipX = false;
            else if (xMotion < 0.01f)
                sr.flipX = true;
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}