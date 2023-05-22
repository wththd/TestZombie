using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;

namespace ZombieGame.Scripts.Runners
{
    public class GameSceneRunner : BaseRunner
    {
        [SerializeField] private GameObject PlayingUI;

        [SerializeField] private GameObject PauseUI;

        [SerializeField] private GameObject DeadUI;

        [SerializeField] private GameObject WonUI;

        private GameProcess _gameProcess;

        protected void Start()
        {
            _gameProcess.StartGame();
        }

        [Inject]
        private void Inject(GameProcess gameProcess)
        {
            _gameProcess = gameProcess;
            _gameProcess.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            DisableUI();

            switch (state)
            {
                case GameState.Playing:
                    PlayingUI.gameObject.SetActive(true);
                    break;
                case GameState.Pause:
                    PauseUI.gameObject.SetActive(true);
                    break;
                case GameState.Dead:
                    DeadUI.gameObject.SetActive(true);
                    break;
                case GameState.Won:
                    WonUI.gameObject.SetActive(true);
                    break;
            }
        }

        private void DisableUI()
        {
            PlayingUI.gameObject.SetActive(false);
            PauseUI.gameObject.SetActive(false);
            DeadUI.gameObject.SetActive(false);
            WonUI.gameObject.SetActive(false);
        }
    }
}