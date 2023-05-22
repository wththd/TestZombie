using System;

namespace ZombieGame.Scripts.Damage
{
    public interface IHealth
    {
        int Current { get; }
        public event Action HealthChanged;
        void TakeDamage(int damage, DamageType damageType);
        void RestoreHealth(int amount);
    }
}