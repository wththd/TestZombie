using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;

namespace ZombieGame.Screens
{
    public class PlayingScreen : MonoBehaviour
    {
        private GameProcess _gameProcess;

        [Inject]
        private void Inject(GameProcess gameProcess)
        {
            _gameProcess = gameProcess;
        }

        public void OnPauseClick()
        {
            _gameProcess.CurrentState = GameState.Pause;
        }
    }
}