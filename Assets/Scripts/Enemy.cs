// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using SpaceShooter;
using System;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Враг.
    /// </summary>
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType
        {
            Base,
            Magic
        }

        #region Parameters

        public event Action OnEnd;

        /// <summary>
        /// Урон, наносимый игроку.
        /// </summary>
        private int damage;

        /// <summary>
        /// Количество золота, которое получит игрок за убийство врага.
        /// </summary>
        private int gold;

        /// <summary>
        /// Количество маны, которое получит игрок за убийство врага.
        /// </summary>
        private int mana;

        /// <summary>
        /// Количество брони у врага.
        /// </summary>
        private int armor;

        private ArmorType armorType;

        private Destructible destructible;

        #endregion

        #region API

        private static Func<int, Projectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, Projectile.DamageType type, int armor) =>
            {   // ArmorType.Base
                switch (type)
                {
                    case Projectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },

            (int power, Projectile.DamageType type, int armor) =>
            {   // ArmorType.Magic

                if (Projectile.DamageType.Base == type)
                    armor = armor / 2;

                return Mathf.Max(power - armor, 1);
            }
        };

        #region Unity API

        private void Start()
        {
            destructible = GetComponent<Destructible>();
        }

        private void OnDestroy()
        {
            OnEnd?.Invoke();

            Sound.EnemyDead.Play();
        }

        #endregion

        #region Public API

        /// <summary>
        /// Применяет настройки на врага.
        /// </summary>
        /// <param name="asset">Настройки врага.</param>
        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            var ship = GetComponent<SpaceShip>();

            ship.MaxLinearVelocity = asset.moveSpeed;

            ship.StartHitPoints = asset.hp;
            ship.ScoreValue = asset.score;

            damage = asset.damage;
            gold = asset.gold;
            mana = asset.mana;
            armorType = asset.armorType;
            armor = asset.armor;
        }

        /// <summary>
        /// Наносит урон игроку.
        /// </summary>
        public void DamagePlayer()
        {
            Player.Instance.LivesCount -= damage;

            Sound.EnemyWin.Play();
        }

        /// <summary>
        /// Выдаёт золото игроку за убийство врага.
        /// </summary>
        public void PlayerGold()
        {
            Player.Instance.AddGold(gold);
            Player.Instance.AddMana(mana);
        }

        public void TakeDamage(int damage, Projectile.DamageType damageType)
        {
            destructible.ApplyDamage(ArmorDamageFunctions[(int)armorType](damage, damageType, armor));
        }

        #endregion

        #endregion
    }
}