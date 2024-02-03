// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;
using TMPro;

#endregion

namespace TowerDefense
{
    /// <summary>
    /// Умения.
    /// </summary>
    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int cost;

            [SerializeField] private int damage;

            [SerializeField] private Color targetingColor;

            public void Start()
            {
                int levelFA = Upgrades.GetUpgradeLevel(Instance.fireAbilityUpgrade);

                if (levelFA > 0)
                {
                    Instance.fireAbilityGO.SetActive(true);
                    damage = damage * levelFA;
                }
                else
                {
                    Instance.fireAbilityGO.SetActive(false);
                }
            }

            public void ManaStatusCheck(int mana)
            {
                if (mana >= cost != Instance.fireAbilityButton.interactable)
                {
                    Instance.fireAbilityButton.interactable = !Instance.fireAbilityButton.interactable;
                    Instance.fireAbilityCostText.color = Instance.fireAbilityButton.interactable ? Color.white : Color.red;
                }
            }

            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;

                    position.z = -Camera.main.transform.position.z;

                    position = Camera.main.ScreenToWorldPoint(position);

                    foreach (var collider in Physics2D.OverlapCircleAll(position, 1))
                    {
                        if (collider.transform.root.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(damage, Projectile.DamageType.Magic);
                        }
                    }
                });

                Player.Instance.RemoveMana(cost);
            }
        }

        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int cost;

            [SerializeField] private float cooldown;

            [SerializeField] private float duration;

            public void Use()
            {
                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }

                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }

                EnemyWaveManager.OnEnemySpawn += Slow;

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.timeButton.interactable = false;
                    yield return new WaitForSeconds(cooldown);
                    Instance.timeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAbilityButton());
            }
        }

        #region Parameters

        [SerializeField] private Button timeButton;

        [SerializeField] private Image targetingCircle;

        [SerializeField] private GameObject clickProtection;

        [SerializeField] private FireAbility fireAbility;

        [SerializeField] private GameObject fireAbilityGO;

        [SerializeField] private Button fireAbilityButton;

        [SerializeField] private TextMeshProUGUI fireAbilityCostText;

        [SerializeField] private TimeAbility timeAbility;

        [SerializeField] private UpgradeAsset fireAbilityUpgrade;

        #endregion

        #region API

        #region Unity API

        private void Start()
        {
            fireAbility.Start();

            Player.Instance.ManaUpdateSubscribe(fireAbility.ManaStatusCheck);
        }

        #endregion

        #region Public API

        public void UseFireAbility()
        {
            fireAbility.Use();
        }

        public void UseTimeAbility()
        {
            timeAbility.Use();
        }

        #endregion

        #endregion
    }
}