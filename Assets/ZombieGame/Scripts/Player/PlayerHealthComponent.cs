using UnityEngine;
using ZombieGame.Scripts.Controllers;
using ZombieGame.Scripts.Damage;

namespace ZombieGame.Scripts.Player
{
    public class PlayerHealthComponent : HealthComponent
    {
        [SerializeField] 
        private PlayerAnimationController playerAnimationController;

        [SerializeField] 
        private PlayerMoveController playerMoveController;
        public override void TakeDamage(int amount, DamageType type)
        {
            base.TakeDamage(amount, type);
            playerAnimationController.PlayHit();
            playerMoveController.OnHit();
        }
    }
}