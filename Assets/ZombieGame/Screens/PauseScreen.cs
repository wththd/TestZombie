using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Screens
{
    public class PauseScreen : MonoBehaviour
    {
        private GameProcess _gameProcess;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Inject(GameProcess gameProcess, SceneLoader sceneLoader)
        {
            _gameProcess = gameProcess;
            _sceneLoader = sceneLoader;
        }

        public void OnContinueClick()
        {
            _gameProcess.CurrentState = GameState.Playing;
        }

        public void OnMenuClick()
        {
            _sceneLoader.Load(SceneNames.Menu);
        }

        public void OnExitClick()
        {
            SceneLoader.CloseGame();
        }
    }
}