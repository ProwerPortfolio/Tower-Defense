// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// ����� �����.
    /// </summary>
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        #region Parameters

        /// <summary>
        /// ���� �����.
        /// </summary>
        [Header("������� ���")]
        public Color color = Color.white;

        /// <summary>
        /// ������� �����.
        /// </summary>
        public Vector2 spriteScale = new Vector2(3, 3);

        /// <summary>
        /// �������� � ������ �����.
        /// </summary>
        public RuntimeAnimatorController animations;



        /// <summary>
        /// �������� �����.
        /// </summary>
        [Header("���������")]
        public float moveSpeed = 1;

        /// <summary>
        /// �������� �����.
        /// </summary>
        public int hp = 5;

        /// <summary>
        /// ��� ����� �����.
        /// </summary>
        public Enemy.ArmorType armorType;

        /// <summary>
        /// ����� �����.
        /// </summary>
        public int armor = 0;

        /// <summary>
        /// ���������� ����� �� �����.
        /// </summary>
        public int score = 10;

        /// <summary>
        /// ����, ��������� ������.
        /// </summary>
        public int damage = 1;

        /// <summary>
        /// ���������� ������, ������� ������� ����� �� �������� �����.
        /// </summary>
        public int gold;

        /// <summary>
        /// ���������� ����, ������� ������� ����� �� �������� �����.
        /// </summary>
        public int mana;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API



        #endregion

        #endregion
    }
}