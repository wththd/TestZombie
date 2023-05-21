using System;
using System.Collections;
using UnityEngine;
using Zenject;

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
        
        [Inject]
        private void Inject(Vector3 position, Vector3 forward, ProjectileSettings settings)
        {
            Transform.position = position;
            Transform.forward = forward;
            _settings = settings;
        }

        private void Awake()
        {
            observer.TriggerEnter += OnHit;
            StartCoroutine(Test());
        }

        private void OnHit(Collider other)
        {
            Debug.Log("On hit " + other.gameObject.name);
            if (other.CompareTag("Enemy"))
            {
                var health = other.gameObject.GetComponent<IHealth>();
                health.TakeDamage(_settings.Damage, _settings.DamageType);
            }
            
            Destroy(gameObject);
        }

        private void Update()
        {
            Transform.position += Transform.forward * (Time.deltaTime * _settings.Speed);
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
        
        public override void DealDamage(int damage)
        {
            
        }
    }
}