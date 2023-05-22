using Zenject;
using ZombieGame.Scripts.Runners;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Installers
{
    public class MenuSceneRunner : BaseRunner
    {
        private SceneLoader _sceneLoader;

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void OnStartGameClick()
        {
            _sceneLoader.Load(SceneNames.Game);
        }

        public void OnExitGameClick()
        {
            SceneLoader.CloseGame();
        }
    }
}