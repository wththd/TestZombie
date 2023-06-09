using System.Collections;
using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Damage.Projectiles;

namespace ZombieGame.Scripts.Damage
{
    public class Bullet : Projectile
    {
        private ProjectileSettings _settings;

        private Transform currentTransform;

        private Transform Transform
        {
            get
            {
                currentTransform ??= GetComponent<Transform>();
                return currentTransform;
            }
        }

        private void Awake()
        {
            observer.TriggerEnter += OnHit;
            StartCoroutine(DestroyRoutine());
        }

        private void Update()
        {
            Transform.position += Transform.forward * (Time.deltaTime * _settings.Speed);
        }

        [Inject]
        private void Inject(Vector3 position, Vector3 forward, ProjectileSettings settings)
        {
            Transform.position = position;
            Transform.forward = forward;
            _settings = settings;
        }

        private void OnHit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var health = other.gameObject.GetComponent<IHealth>();
                health.TakeDamage(_settings.Damage, _settings.DamageType);
            }

            Destroy(gameObject);
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(_settings.Timer);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            observer.TriggerEnter -= OnHit;
        }
    }
}