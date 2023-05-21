using UnityEngine;
using ZombieGame.Scripts.Controllers;
using ZombieGame.Scripts.Damage;

namespace ZombieGame.Scripts.Player
{
    public class PlayerDeathComponent : DeathComponent
    {
        [SerializeField] 
        private PlayerAnimationController playerAnimationController;
        [SerializeField] 
        private PlayerMoveController playerMoveController;
        [SerializeField] 
        private PlayerFire playerFire;
        
        protected override void OnHealthChanged()
        {
            if (health.Current <= 0)
            {
                playerAnimationController.PlayDeath();
                playerMoveController.OnDeath();
                playerFire.OnDeath();
            }
        }
    }
}