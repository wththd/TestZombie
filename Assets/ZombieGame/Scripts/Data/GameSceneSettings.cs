using System;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Damage.Projectiles;
using ZombieGame.Scripts.Enemy;
using ZombieGame.Scripts.Player;

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