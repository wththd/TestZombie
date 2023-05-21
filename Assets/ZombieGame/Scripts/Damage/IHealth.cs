using System;

namespace ZombieGame.Scripts.Damage
{
    public interface IHealth
    {
        public event Action HealthChanged;
        void TakeDamage(int damage, DamageType damageType);
        void RestoreHealth(int amount);
        int Current { get; }
    }
}