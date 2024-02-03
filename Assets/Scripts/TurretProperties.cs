using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary,
        Auto
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode mode;
        public TurretMode Mode => mode;

        [SerializeField] private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;

        [SerializeField] private float rateOfFire;
        public float RateOfFire => rateOfFire;

        [SerializeField] private int energyUsage;
        public int EnergyUsage => energyUsage;

        [SerializeField] private int ammoUsage;
        public int AmmoUsage => ammoUsage;

        [SerializeField] private AudioClip launchSFX;
        public AudioClip LaunchSFX => launchSFX;
    }
}