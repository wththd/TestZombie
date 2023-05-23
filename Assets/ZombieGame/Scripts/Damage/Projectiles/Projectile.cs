using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Observers;

namespace ZombieGame.Scripts.Damage.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] 
        protected TriggerObserver observer;

        public class Factory : PlaceholderFactory<Vector3, Vector3, ProjectileSettings, Projectile>
        {
        }
    }
}