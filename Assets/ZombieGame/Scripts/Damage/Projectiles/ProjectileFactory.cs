using System;
using UnityEngine;
using Zenject;

namespace ZombieGame.Scripts.Damage
{
    public class ProjectileFactory : IFactory<Vector3, Vector3, ProjectileSettings, Projectile>
    {
        private readonly DiContainer _container;
        private readonly Settings _settings;

        public ProjectileFactory(Settings settings, DiContainer container)
        {
            _settings = settings;
            _container = container;
        }

        public Projectile Create(Vector3 position, Vector3 direction, ProjectileSettings settings)
        {
            Projectile prefab = null;
            switch (settings.ProjectileType)
            {
                case ProjectileType.Bullet:
                    prefab = _settings.BulletPrefab;
                    break;
                default:
                    throw new NotImplementedException();
            }

            var args = new object[] { position, direction, settings };
            var instance = _container.InstantiatePrefabForComponent<Projectile>(prefab, args);

            return instance;
        }

        [Serializable]
        public class Settings
        {
            public Bullet BulletPrefab;
        }
    }
}