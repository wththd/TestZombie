using System;
using UnityEngine;

namespace ZombieGame.Scripts.Damage
{
    [RequireComponent(typeof(IHealth))]
    public abstract class DeathComponent : MonoBehaviour
    {
        protected IHealth health;
        protected virtual void Awake()
        {
            health = GetComponent<IHealth>();
            health.HealthChanged += OnHealthChanged;
        }

        protected void OnDestroy()
        {
            health.HealthChanged -= OnHealthChanged;
        }

        protected abstract void OnHealthChanged();
    }
}