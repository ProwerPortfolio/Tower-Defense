// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System;
using TowerDefense;
using UnityEngine.SceneManagement;

#endregion

namespace SpaceShooter
{
    public class Player : MonoSingleton<Player>
    {
        #region Parameters

        /// <summary>
        /// ������� ��������� ���������� ������.
        /// </summary>
        private event Action<int> OnGoldUpdate;

        /// <summary>
        /// ������� ��������� ���������� ����.
        /// </summary>
        private event Action<int> OnManaUpdate;

        /// <summary>
        /// ������� ��������� ���������� ��������.
        /// </summary>
        private event Action<int> OnHPUpdate;

        /// <summary>
        /// ���������� ������ ������.
        /// </summary>
        [SerializeField] private int livesCount;

        private int healthDef = 0;

        /// <summary>
        /// ������� ���������� ������ ������.
        /// </summary>
        private int gold;

        /// <summary>
        /// ������� ���������� ���� ������.
        /// </summary>
        private int mana;

        /// <summary>
        /// ������ �� ������� ������� ������.
        /// </summary>
        [SerializeField] private SpaceShip ship;
        /// <summary>
        /// ������ �� ������ ������� ������.
        /// </summary>
        [SerializeField] private GameObject playerShipPrefab;

        /// <summary>
        /// ������� �� �����, ������� ����� �������.
        /// </summary>
        [SerializeField] private Tower towerPrefab;

        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private UpgradeAsset healthDefenderUpgrade;

        private int upgradeLevelMM = 1;

        /// <summary>
        /// ������� �� ����� ����������� �� ����?
        /// </summary>
        public bool isDamaged = false;

        /// <summary>
        /// ���������� �� �������� ��� ������� ������?
        /// </summary>
        public bool healthIsChanged = false;

        /*
        /// <summary>
        /// ������ �� CameraController.
        /// </summary>
        [SerializeField] private CameraController cameraController;
        /// <summary>
        /// ������ �� MovementController.
        /// </summary>
        [SerializeField] private MovementController movementController;
        */

        #endregion

        #region API

        /// <summary>
        /// ��������� ������ ��� ����������� ����, ���� ������ �� ��������.
        /// </summary>
        private void OnShipDead()
        {
            livesCount--;

            if (livesCount > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                ship = newPlayerShip.GetComponent<SpaceShip>();

                // cameraController.SetTarget(ship.transform);

                // movementController.SetTargetShip(ship);

                ship.EventOnDeath.AddListener(OnShipDead);
            }
        }

        #region Unity API

        /// <summary>
        /// ��������� ������� ������� � ������ � ���������� ���, ���� �� ����.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (ship != null)
                Destroy(ship.gameObject);
        }

        private void Start()
        {
            Respawn();

            var levelH = Upgrades.GetUpgradeLevel(healthUpgrade);

            int upgradeLevelH = levelH + 1;

            livesCount = upgradeLevelH * 5;
            OnHPUpdate?.Invoke(livesCount);

            var levelHD = Upgrades.GetUpgradeLevel(healthDefenderUpgrade);

            healthDef = levelHD;
        }

        #endregion

        #region Public API

        public SpaceShip ActiveShip => ship;

        public int LivesCount
        {
            get => livesCount;

            set
            {
                if (livesCount <= 0) return;

                if (value < livesCount)
                    isDamaged = true;
                

                if (isDamaged && healthDef <= 0)
                    livesCount = value;
                else
                    healthDef--;

                OnHPUpdate(livesCount);

                if (livesCount <= 0)
                {
                    LevelSequenceController.Instance.FinishCurrentLevel(false);

                    Sound.PlayerLose.Play();
                }
            }
        }

        public void GoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.gold);
        }

        public void ManaUpdateSubscribe(Action<int> action)
        {
            OnManaUpdate += action;
            action(Instance.mana);
        }

        public void HPUpdateSubscribe(Action<int> action)
        {
            OnHPUpdate += action;
            action(Instance.livesCount);
        }

        /// <summary>
        /// �������� ����������� ���������� ������ � ������.
        /// </summary>
        /// <param name="goldCount">���������� ���������� ������.</param>
        /// <returns>������� �� ������?</returns>
        public bool RemoveGold(int goldCount)
        {
            if (gold - goldCount < 0) return false;

            gold -= goldCount;

            OnGoldUpdate(gold);

            return true;
        }

        /// <summary>
        /// ������������� ����� ���������� ������ ������.
        /// </summary>
        /// <param name="goldCount"></param>
        public void SetGold(int goldCount)
        {
            if (goldCount < 0) return;

            gold = goldCount;

            OnGoldUpdate(gold);
        }

        /// <summary>
        /// ��������� ����������� ���������� ������ ������.
        /// </summary>
        /// <param name="goldCount">����������� ���������� ������.</param>
        public void AddGold(int goldCount)
        {
            gold += goldCount;

            OnGoldUpdate(gold);
        }


        /// <summary>
        /// �������� ����������� ���������� ���� � ������.
        /// </summary>
        /// <param name="manaCount">���������� ���������� ����.</param>
        /// <returns>������� �� ������?</returns>
        public bool RemoveMana(int manaCount)
        {
            if (mana - manaCount < 0) return false;

            mana -= manaCount;

            OnManaUpdate(mana);

            return true;
        }

        /// <summary>
        /// ������������� ����� ���������� ���� ������.
        /// </summary>
        /// <param name="manaCount"></param>
        public void SetMana(int manaCount)
        {
            if (manaCount < 0) return;

            mana = manaCount;

            OnManaUpdate(mana);
        }

        /// <summary>
        /// ��������� ����������� ���������� ���� ������.
        /// </summary>
        /// <param name="manaCount">����������� ���������� ����.</param>
        public void AddMana(int manaCount)
        {
            mana += manaCount;

            OnManaUpdate(mana);
        }

        /// <summary>
        /// ������� ��������� ����� � ����������� �� ���������� ������.
        /// </summary>
        /// <param name="towerAsset"></param>
        /// <param name="buildSite"></param>
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            RemoveGold(towerAsset.goldCost);

            var tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);

            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;

            tower.GetComponentInChildren<Turret>().turretProperties = towerAsset.turretProperties;

            var towerBuildSite = GetComponentInChildren<BuildSite>();

            if (towerBuildSite)
                towerBuildSite.SetBuildableTowers(towerAsset.upgradesTo);

            Destroy(buildSite.gameObject);
        }

        #region Score

        /// <summary>
        /// ���������� ����� ������.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// ���������� ������� ������.
        /// </summary>
        public int KillCount { get; private set; }

        /// <summary>
        /// ��������� ���� ��������.
        /// </summary>
        public void AddKill()
        {
            KillCount++;
        }

        /// <summary>
        /// ��������� ����������� ���������� �����.
        /// </summary>
        /// <param name="count">���������� �����.</param>
        public void AddScore(int count)
        {
            Score += count;
        }

        #endregion

        #endregion

        #endregion
    }
}