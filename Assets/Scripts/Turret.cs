using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        #region Параметры

        [SerializeField] private TurretMode mode;
        public TurretMode Mode => mode;

        public TurretProperties turretProperties;

        private float refireTimer;

        public bool IsCanFire => refireTimer <= 0;

        private SpaceShip ship;

        #endregion

        #region Методы Unity

        private void Start()
        {
            ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (refireTimer > 0)
                refireTimer -= Time.deltaTime;
            else if (Mode == TurretMode.Auto)
                Fire();
        }

        #endregion

        #region Публичное API

        public void Fire()
        {
            if (turretProperties == null) return;

            if (!IsCanFire) return;

            if (ship)
            {
                if (ship.DrawEnergy(turretProperties.EnergyUsage) == false) return;

                if (ship.DrawAmmo(turretProperties.AmmoUsage) == false) return;
            }

            Projectile projectile = Instantiate(turretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            Sound.Arrow.Play();

            if (ship)
                projectile.parentID = ship.ID;

            refireTimer = turretProperties.RateOfFire;

            // SFX
        }

        public void AssignLoadout(TurretProperties properties)
        {
            if (mode != properties.Mode) return;

            refireTimer = 0;
            turretProperties = properties;
        }

        #endregion
    }
}