using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Data;
using ZombieGame.Scripts.Player;
using ZombieGame.Scripts.Systems.Pause;

namespace ZombieGame.Scripts.Systems
{
    public class GameProcess : IInitializable
    {
        private readonly EnemySpawner.Factory _enemySpawnerFactory;
        private readonly Camera _mainCamera;
        private readonly IPauseController _pauseController;

        private readonly PlayerMoveController.Factory _playerFactory;
        private GameState currentState;
        private int currentWave;

        private EnemySpawner enemySpawner;
        private readonly int maxWaves;
        private PlayerMoveController player;

        public GameProcess(PlayerMoveController.Factory playerFactory, Camera mainCamera,
            EnemySpawner.Factory enemySpawnerFactory, List<WaveConfig> waves, IPauseController pauseController)
        {
            _playerFactory = playerFactory;
            _mainCamera = mainCamera;
            _enemySpawnerFactory = enemySpawnerFactory;
            _pauseController = pauseController;
            maxWaves = waves.Count;

            GameStateChanged += OnGameStateChanged;
        }

        public GameState CurrentState
        {
            get => currentState;
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    GameStateChanged?.Invoke(currentState);
                }
            }
        }

        public void Initialize()
        {
            player = _playerFactory.Create();
            _mainCamera.GetComponent<CameraFollowController>().Follow(player.gameObject);
            enemySpawner = _enemySpawnerFactory.Create(player);
            enemySpawner.WaveCleared += OnWaveCleared;
        }

        public event Action<GameState> GameStateChanged;

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.Playing)
                _pauseController.Resume();
            else
                _pauseController.Pause();
        }

        public void StartGame()
        {
            currentWave = 0;
            enemySpawner.StartWave(currentWave);
            CurrentState = GameState.Playing;
        }

        private void OnWaveCleared()
        {
            if (currentWave + 1 < maxWaves)
            {
                currentWave++;
                enemySpawner.StartWave(currentWave);
            }
            else
            {
                CurrentState = GameState.Won;
            }
        }

        public void Retry()
        {
            DisposeCurrentGame();
            Initialize();
            StartGame();
        }

        private void DisposeCurrentGame()
        {
            player.Dispose();
            enemySpawner.ClearWave();
            enemySpawner.WaveCleared -= OnWaveCleared;
        }
    }
}