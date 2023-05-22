using System.Collections;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Runners
{
    public class SplashScreenRunner : BaseRunner
    {
        private SceneLoader _sceneLoader;

        protected override void Awake()
        {
            StartCoroutine(WaitSplashAndLoadMenu());
        }

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private IEnumerator WaitSplashAndLoadMenu()
        {
            yield return new WaitForSeconds(2);
            _sceneLoader.Load(SceneNames.Menu);
        }
    }
}