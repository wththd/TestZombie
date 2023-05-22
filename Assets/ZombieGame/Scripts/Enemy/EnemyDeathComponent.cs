using System;
using UnityEngine;
using ZombieGame.Scripts.Damage;

namespace ZombieGame.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeathComponent : DeathComponent
    {
        private EnemyAnimator enemyAnimator;

        protected override void Awake()
        {
            base.Awake();
            enemyAnimator = GetComponent<EnemyAnimator>();
        }

        public event Action Died;

        protected override void OnHealthChanged()
        {
            if (health.Current <= 0)
            {
                enemyAnimator.PlayDeath();
                Died?.Invoke();
            }
        }
    }
}