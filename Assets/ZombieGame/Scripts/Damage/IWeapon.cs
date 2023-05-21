using UnityEngine;

namespace ZombieGame.Scripts.Damage
{
    public interface IWeapon
    {
        ProjectileSettings WeaponSettings { get; }
        float Cooldown { get; }
        int Capacity { get; }
        int CurrentCapacity { get; }
        Transform FirePoint { get; }
        void Fire();
        void Reload();
        bool CanFire();
    }
}