using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Data;

namespace ZombieGame.Scripts.Installers
{
    [CreateAssetMenu]
    public class GameScriptableObjectInstaller : ScriptableObjectInstaller
    {
        public List<WaveConfig> WaveConfigs;

        public override void InstallBindings()
        {
            Container.BindInstance(WaveConfigs);
        }
    }
}