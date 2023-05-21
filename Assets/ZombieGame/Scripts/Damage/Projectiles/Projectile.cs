using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Enemy;

namespace ZombieGame.Scripts.Damage
{
    public abstract class Projectile : MonoBehaviour, IDamage
    {
        [SerializeField]
        protected Rigidbody rb;
        [SerializeField] 
        protected TriggerObserver observer;
        public abstract void DealDamage(int damage);

        public class Factory : PlaceholderFactory<Vector3, Vector3, ProjectileSettings, Projectile>
        {
        }
    }
}