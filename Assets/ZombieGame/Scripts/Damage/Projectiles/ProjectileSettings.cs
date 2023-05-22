using System;

namespace ZombieGame.Scripts.Damage.Projectiles
{
    [Serializable]
    public struct ProjectileSettings
    {
        public ProjectileType ProjectileType;
        public float Speed;
        public int Damage;
        public DamageType DamageType;
        public int Timer;
    }
}