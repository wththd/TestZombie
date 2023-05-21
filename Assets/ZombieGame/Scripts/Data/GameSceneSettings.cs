using System;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Enemy;

namespace ZombieGame.Scripts.Data
{
    [Serializable]
    public class GameSceneSettings
    {
        public EnemyController EnemyControllerPrefab;
        public PlayerMoveController PlayerPrefab;
        public ProjectileFactory.Settings ProjectileFactorySettings;
    }
}