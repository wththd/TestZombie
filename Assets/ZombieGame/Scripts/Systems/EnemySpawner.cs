using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Data;
using ZombieGame.Scripts.Enemy;
using ZombieGame.Scripts.Utils;
using Random = System.Random;

namespace ZombieGame.Scripts.Systems
{
    public class EnemySpawner
    {
        private readonly Camera _camera;

        private readonly EnemyController.Factory _enemyFactory;
        private readonly PlayerMoveController _playerMoveController;
        private readonly SpawnBounds _spawnBounds;
        private readonly List<WaveConfig> _waveConfigs;

        private readonly List<EnemyController> enemies = new();

        private WaveConfig currentConfig;
        private int enemiesDied;

        public EnemySpawner(PlayerMoveController playerMoveController, Camera camera, List<WaveConfig> waveConfigs,
            EnemyController.Factory enemyFactory, SpawnBounds spawnBounds)
        {
            _playerMoveController = playerMoveController;
            _waveConfigs = waveConfigs;
            _enemyFactory = enemyFactory;
            _camera = camera;
            _spawnBounds = spawnBounds;
        }

        private int EnemiesLeft => currentConfig.TotalEnemies - enemiesDied;
        public event Action WaveCleared;

        public void StartWave(int waveNumber)
        {
            ClearWave();
            enemiesDied = 0;
            currentConfig = _waveConfigs[waveNumber];
            for (var i = 0; i < currentConfig.MaxEnemies; i++) SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var randomPosition = GetPositionOutsideCamera();
            var enemy = _enemyFactory.Create(randomPosition, _playerMoveController.transform, 1.7f);
            if (enemy.CanDie) enemy.Died += OnEnemyDied;

            enemies.Add(enemy);
        }

        private void OnEnemyDied(EnemyController enemy)
        {
            enemy.Died -= OnEnemyDied;
            enemy.Dispose();
            enemies.Remove(enemy);
            enemiesDied++;
            if (EnemiesLeft > 0 && EnemiesLeft >= currentConfig.MaxEnemies)
                SpawnEnemy();
            else if (EnemiesLeft == 0) WaveCleared?.Invoke();
        }

        private Vector3 GetPositionOutsideCamera()
        {
            while (true)
            {
                var random = new Random();
                var left = random.NextDouble() < 0.5f;
                var top = random.NextDouble() > 0.5f;
                var startX = left ? 0 - random.NextDouble() : 1 + random.NextDouble();
                var startY = top ? 1 + random.NextDouble() : 0 - random.NextDouble();
                var randomPoint =
                    _camera.ViewportToWorldPoint(
                        new Vector3((float)startX, (float)startY, _camera.transform.position.y));
                if (!_spawnBounds.IsInRange(randomPoint)) continue;

                return randomPoint;
            }
        }

        public void ClearWave()
        {
            foreach (var enemy in enemies)
            {
                enemy.Died -= OnEnemyDied;
                enemy.Despawn();
            }

            enemies.Clear();
        }

        public class Factory : PlaceholderFactory<PlayerMoveController, EnemySpawner>
        {
        }
    }
}