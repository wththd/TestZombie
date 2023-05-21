using System;
using UnityEngine;

namespace ZombieGame.Scripts.Damage
{
    public abstract class HealthComponent : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;

        [SerializeField] 
        protected int initialHealth;

        public int Current { get; private set; }

        protected virtual void OnEnable()
        {
            Current = initialHealth;
        }
        
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