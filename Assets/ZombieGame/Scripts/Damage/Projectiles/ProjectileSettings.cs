using System;

namespace ZombieGame.Scripts.Damage
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