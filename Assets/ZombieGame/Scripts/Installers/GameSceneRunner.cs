using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Data;
using ZombieGame.Scripts.Enemy;
using ZombieGame.Scripts.Player;

namespace ZombieGame.Scripts.Installers
{
    public class GameSceneRunner : BaseRunner
    {
        private PlayerMoveController.Factory _playerFactory;
        private EnemySpawner.Factory _enemySpawnerFactory;
        private Camera _mainCamera;

        private EnemySpawner enemySpawner;

        [Inject]
        private void Inject(PlayerMoveController.Factory playerFactory, Camera mainCamera, EnemySpawner.Factory enemySpawnerFactory)
        {
            _playerFactory = playerFactory;
            _mainCamera = mainCamera;
            _enemySpawnerFactory = enemySpawnerFactory;
        }

        protected override void Awake()
        {
            var player = _playerFactory.Create();
            _mainCamera.GetComponent<CameraFollowController>().Follow(player.gameObject);
            var config = ScriptableObject.CreateInstance<WaveConfig>();
            config.MaxEnemies = 30;
            config.TotalEnemies = 50;
            enemySpawner = _enemySpawnerFactory.Create(player, config);
            enemySpawner.StartWave();
        }
    }
}