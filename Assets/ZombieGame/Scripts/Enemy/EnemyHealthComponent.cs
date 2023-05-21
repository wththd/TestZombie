using ZombieGame.Scripts.Damage;

namespace ZombieGame.Scripts.Enemy
{
    public class EnemyHealthComponent : HealthComponent
    {
        private EnemyAnimator enemyAnimator;
        private EnemyController enemyController;

        protected void Awake()
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
        }

        public override void TakeDamage(int amount, DamageType type)
        {
            base.TakeDamage(amount, type);
            enemyAnimator.PlayHit();
            enemyController.OnHit();
        }
    }
}