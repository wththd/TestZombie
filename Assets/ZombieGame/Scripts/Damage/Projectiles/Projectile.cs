using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Observers;

namespace ZombieGame.Scripts.Damage.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IDamage
    {
        [SerializeField] protected TriggerObserver observer;

        public abstract void DealDamage(int damage);

        public class Factory : PlaceholderFactory<Vector3, Vector3, ProjectileSettings, Projectile>
        {
        }
    }
}