// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������ �� �����, ������� ����� ����� ���������� ��������.
    /// </summary>
    public class Destructible : Entity
    {
        #region Parameters

        public const int TEAM_ID_NEUTRAL = 0;

        /// <summary>
        /// ����� �� ������ �������� ����?
        /// </summary>
        [SerializeField] private bool isDestructible;

        /// <summary>
        /// ��������� ���������� ��������.
        /// </summary>
        [SerializeField] private int startHitPoints;

        [SerializeField] private GameObject destroyEffectPrefab;

        public int teamId;

        [SerializeField] private int scoreValue;

        [SerializeField] protected UnityEvent eventOnDeath;

        /// <summary>
        /// ������� ���������� ��������.
        /// </summary>
        private int currentHitPoints;

        private float timer;

        private static HashSet<Destructible> allDestructibles;

        #endregion

        #region API

        /// <summary>
        /// ���������������� �����, ������� ���������� � ������ ����������� �������, �� ���� ����� ���������� ��� �������� ������ ���� ����� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            SpawnDestroyEffect();
            eventOnDeath?.Invoke();
        }

        protected virtual void OnDeath(int parentID)
        {
            OnDeath();

           // if (parentID == Player.Instance.ActiveShip.ID)
            //    Player.Instance.AddKill();
        }

        protected void SpawnDestroyEffect()
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            effect.transform.localScale = transform.localScale;
            Destroy(effect, 0.7f);
        }

        #region Unity API

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= 2)
            {
                isDestructible = true;
            }
        }

        protected virtual void Start()
        {
            currentHitPoints = startHitPoints;
        }

        protected virtual void OnEnable()
        {
            if (allDestructibles == null)
                allDestructibles = new HashSet<Destructible>();

            allDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            allDestructibles.Remove(this);
        }

        #endregion

        #region Public API

        public bool IsDestructible => isDestructible;

        public int StartHitPoints
        {
            get => startHitPoints;

            set
            {
                if (value < 0) return;

                startHitPoints = value;
            }
        }

        public int CurrentHitPoints => currentHitPoints;

        public UnityEvent EventOnDeath => eventOnDeath;

        public static IReadOnlyCollection<Destructible> AllDestructibles => allDestructibles;

        public int TeamId => teamId;

        public int ScoreValue
        {
            get => scoreValue;

            set
            {
                if (value < 0) return;

                scoreValue = value;
            }
        }

        public int HitPoints => startHitPoints;

        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage"> ���������� �����. </param>
        public void ApplyDamage(int damage)
        {
            if (!isDestructible) return;

            currentHitPoints -= damage;

            if (currentHitPoints <= 0)
                OnDeath();
        }

        public void ApplyDamage(int damage, int parentID)
        {
            if (!isDestructible) return;

            currentHitPoints -= damage;

            if (currentHitPoints <= 0)
                OnDeath(parentID);
        }

        /// <summary>
        /// ��������� ���������� �������� � �������.
        /// </summary>
        /// <param name="value"> ����� �������� ��������. </param>
        public void SetHealth(int value)
        {
            currentHitPoints = value;

            if (currentHitPoints <= 0)
                OnDeath();
        }

        public void DontDestroyEnable()
        {
            isDestructible = false;
            timer = 0;
        }

        #endregion

        #endregion
    }
}