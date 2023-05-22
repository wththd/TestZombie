using System;
using UnityEngine;

namespace ZombieGame.Scripts.Damage
{
    public abstract class HealthComponent : MonoBehaviour, IHealth
    {
        [SerializeField] protected int initialHealth;

        protected virtual void OnEnable()
        {
            Current = initialHealth;
        }

        public event Action HealthChanged;

        public int Current { get; private set; }

        public virtual void TakeDamage(int damage, DamageType damageType)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }

        public virtual void RestoreHealth(int amount)
        {
            Current = Math.Min(Current + amount, initialHealth);
        }
    }
}