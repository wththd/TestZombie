using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Data;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Enemy
{
    public class EnemySpawner
    {
        private List<EnemyController> enemies = new();

        private readonly EnemyController.Factory _enemyFactory;
        private readonly WaveConfig _waveConfig;
        private readonly Camera _camera;
        private readonly PlayerMoveController _playerMoveController;
        private readonly SpawnBounds _spawnBounds;

        private int EnemiesLeft => _waveConfig.TotalEnemies - enemiesDied;
        private int enemiesDied;

        public EnemySpawner(PlayerMoveController playerMoveController, WaveConfig waveConfig, Camera camera, EnemyController.Factory enemyFactory, SpawnBounds spawnBounds)
        {
            _playerMoveController = playerMoveController;
            _waveConfig = waveConfig;
            _enemyFactory = enemyFactory;
            _camera = camera;
            _spawnBounds = spawnBounds;
        }

        public void StartWave()
        {
            for (var i = 0; i < _waveConfig.MaxEnemies; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var randomPosition = GetPositionOutsideCamera();
            var enemy = _enemyFactory.Create(randomPosition, _playerMoveController.transform, 2);
            if (enemy.CanDie)
            {
                enemy.Died += OnEnemyDied;
            }

            enemies.Add(enemy);
        }

        private void OnEnemyDied(EnemyController enemy)
        {
            enemy.Died -= OnEnemyDied;
            enemy.Dispose();
            enemies.Remove(enemy);
            enemiesDied++;
            if (EnemiesLeft > 0)
            {
                SpawnEnemy();
            }
        }

        private Vector3 GetPositionOutsideCamera()
        {
            while (true)
            {
                var random = new System.Random();
                var left = random.NextDouble() < 0.5f;
                var top = random.NextDouble() > 0.5f;
                var startX = left ? 0 - random.NextDouble() : 1 + random.NextDouble();
                var startY = top ? 1 + random.NextDouble() : 0 - random.NextDouble();
                var randomPoint =
                    _camera.ViewportToWorldPoint(
                        new Vector3((float)startX, (float)startY, _camera.transform.position.y));
                if (!_spawnBounds.IsInRange(randomPoint))
                {
                    continue;
                }
                
                return randomPoint;
            }
        }

        public class Factory : PlaceholderFactory<PlayerMoveController, WaveConfig, EnemySpawner>
        {
        }
    }
}