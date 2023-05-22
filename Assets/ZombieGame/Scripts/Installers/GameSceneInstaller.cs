using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Damage.Projectiles;
using ZombieGame.Scripts.Data;
using ZombieGame.Scripts.Enemy;
using ZombieGame.Scripts.Player;
using ZombieGame.Scripts.Services;
using ZombieGame.Scripts.Services.InputService;
using ZombieGame.Scripts.Systems;
using ZombieGame.Scripts.Utils;

namespace ZombieGame.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameSceneSettings gameSceneSettings;

        [SerializeField] private Transform playerSpawnTransform;

        [SerializeField] private Transform zombiesSpawnTransform;

        [SerializeField] private Transform leftBorderPosition;
        [SerializeField] private Transform rightBorderPosition;

        [SerializeField] private Camera mainCamera;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSceneSettings.ProjectileFactorySettings);
            Container.BindInstance(mainCamera);
            Container.Bind<IInputService>().To<StandaloneInput>().AsSingle();
            Container.Bind<SpawnBounds>().AsSingle()
                .WithArguments(leftBorderPosition.position, rightBorderPosition.position);
            Container.BindInterfacesAndSelfTo<GameProcess>().AsSingle();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindFactory<Vector3, Transform, float, EnemyController, EnemyController.Factory>()
                .FromMonoPoolableMemoryPool(pool => pool.WithInitialSize(40)
                    .FromComponentInNewPrefab(gameSceneSettings.EnemyControllerPrefab)
                    .UnderTransform(zombiesSpawnTransform));
            Container.BindFactory<Vector3, Vector3, ProjectileSettings, Projectile, Projectile.Factory>()
                .FromFactory<ProjectileFactory>();
            Container.BindFactory<PlayerMoveController, PlayerMoveController.Factory>()
                .FromComponentInNewPrefab(gameSceneSettings.PlayerPrefab)
                .UnderTransform(playerSpawnTransform);
            Container.BindFactory<PlayerMoveController, EnemySpawner, EnemySpawner.Factory>();
        }
    }
}