using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Controllers;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Systems;

namespace ZombieGame.Scripts.Player
{
    public class PlayerDeathComponent : DeathComponent
    {
        [SerializeField] private PlayerAnimationController playerAnimationController;

        [SerializeField] private PlayerMoveController playerMoveController;

        [SerializeField] private PlayerFire playerFire;

        private GameProcess _gameProcess;

        [Inject]
        public void Inject(GameProcess gameProcess)
        {
            _gameProcess = gameProcess;
        }

        protected override void OnHealthChanged()
        {
            if (health.Current <= 0)
            {
                playerAnimationController.PlayDeath();
                playerMoveController.OnDeath();
                playerFire.OnDeath();
                _gameProcess.CurrentState = GameState.Dead;
            }
        }
    }
}