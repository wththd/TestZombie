using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Screens
{
    public class WonScreen : MonoBehaviour
    {
        private GameProcess _gameProcess;

        [Inject]
        private void Inject(GameProcess gameProcess)
        {
            _gameProcess = gameProcess;
        }

        public void OnRetryClick()
        {
            _gameProcess.Retry();
        }

        public void OnExitClick()
        {
            SceneLoader.CloseGame();
        }
    }
}