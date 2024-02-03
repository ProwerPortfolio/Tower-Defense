// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace TowerDefense
{
    /// <summary>
    /// �����, ��������� ������.
    /// </summary>
    public class Tower : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ ����������� �����.
        /// </summary>
        [SerializeField] private float radius;

        /// <summary>
        /// ������ ���� ������� �� �����.
        /// </summary>
        private Turret[] turrets;

        /// <summary>
        /// ������� ���� �����.
        /// </summary>
        private Destructible target;

        /// <summary>
        /// ������� ���� ����� � ������ ����������.
        /// </summary>
        private Vector3 targetPoint;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        /// <summary>
        /// ���������, �� ����� �� ���� � ������� � ��������, ���� ��� ���.
        /// </summary>
        private void Update()
        {
            if (target)
            {
                targetPoint = target.transform.position + (Vector3)target?.GetComponent<Rigidbody2D>().velocity;

                if (Vector3.Distance(target.transform.position, transform.position) <= radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position;

                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }                
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = new Color(0, 255, 255, 0.2f);

            Handles.DrawSolidDisc(transform.position, transform.forward, radius);
        }
#endif

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}