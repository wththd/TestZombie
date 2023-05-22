using UnityEngine;
using ZombieGame.Scripts.Damage.Projectiles;

namespace ZombieGame.Scripts.Damage
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [Tooltip("Weapon projectile settings")] 
        [SerializeField]
        private ProjectileSettings settings;

        [Tooltip("Cooldown between firing a projectile")]
        [SerializeField]
        private float cooldown;

        [Tooltip("Weapon ammo capacity")] 
        [SerializeField]
        private int capacity;

        [Tooltip("Weapon starting fire point")] 
        [SerializeField]
        private Transform firePoint;

        public Transform FirePoint => firePoint;

        public ProjectileSettings WeaponSettings => settings;
        public float Cooldown => cooldown;
        public int Capacity => capacity;
        public int CurrentCapacity { get; private set; }

        public bool CanFire()
        {
            return Capacity == 0 || CurrentCapacity > 0;
        }

        public void Fire()
        {
            if (Capacity == 0) return;

            if (CurrentCapacity > 0) CurrentCapacity--;
        }

        public void Reload()
        {
            CurrentCapacity = Capacity;
        }
    }
}