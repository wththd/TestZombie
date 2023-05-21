using System.Collections;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Installers
{
    public class ProjectContextInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromComponentOn(gameObject);
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle();
        }

        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
    }
}