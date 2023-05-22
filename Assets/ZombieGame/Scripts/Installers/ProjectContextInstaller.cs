using System.Collections;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Systems;
using ZombieGame.Scripts.Systems.Pause;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Installers
{
    public class ProjectContextInstaller : MonoInstaller, ICoroutineRunner
    {
        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this);
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<IPauseController>().To<TimeScalePauseController>().AsSingle();
        }
    }
}