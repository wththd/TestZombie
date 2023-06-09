using ZombieGame.Scripts.Damage.Projectiles;

namespace ZombieGame.Scripts.Damage
{
    public class Gun : Weapon
    {
        public ProjectileSettings WeaponSettings { get; }
        public float Cooldown { get; }
        public int Capacity { get; }
    }
}