using System;
using UnityEngine;
using ZombieGame.Scripts.Damage;

namespace ZombieGame.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeathComponent : DeathComponent
    {
        public event Action Died;

        private EnemyAnimator enemyAnimator;
        protected override void Awake()
        {
            base.Awake();
            enemyAnimator = GetComponent<EnemyAnimator>();
        }

        protected override void OnHealthChanged()
        {
            Debug.Log($"OnHealthChanged EnemyDeathComponent {health.Current}");
            if (health.Current <= 0)
            {
                enemyAnimator.PlayDeath();
                Died?.Invoke();
            }
        }
    }
}