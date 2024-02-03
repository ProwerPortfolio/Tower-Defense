// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Ассет врага.
    /// </summary>
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        #region Parameters

        /// <summary>
        /// Цвет врага.
        /// </summary>
        [Header("Внешний вид")]
        public Color color = Color.white;

        /// <summary>
        /// Размеры врага.
        /// </summary>
        public Vector2 spriteScale = new Vector2(3, 3);

        /// <summary>
        /// Анимация и спрайт врага.
        /// </summary>
        public RuntimeAnimatorController animations;



        /// <summary>
        /// Скорость врага.
        /// </summary>
        [Header("Параметры")]
        public float moveSpeed = 1;

        /// <summary>
        /// Здоровье врага.
        /// </summary>
        public int hp = 5;

        /// <summary>
        /// Тип брони врага.
        /// </summary>
        public Enemy.ArmorType armorType;

        /// <summary>
        /// Броня врага.
        /// </summary>
        public int armor = 0;

        /// <summary>
        /// Количество очков за врага.
        /// </summary>
        public int score = 10;

        /// <summary>
        /// Урон, наносимый игроку.
        /// </summary>
        public int damage = 1;

        /// <summary>
        /// Количество золота, которое получит игрок за убийство врага.
        /// </summary>
        public int gold;

        /// <summary>
        /// Количество маны, которое получит игрок за убийство врага.
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