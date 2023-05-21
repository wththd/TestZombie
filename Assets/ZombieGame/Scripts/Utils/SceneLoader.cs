using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ZombieGame.Scripts.Utils
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _runner;
        
        public SceneLoader(ICoroutineRunner runner)
        {
            _runner = runner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _runner.RunCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
      
            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}