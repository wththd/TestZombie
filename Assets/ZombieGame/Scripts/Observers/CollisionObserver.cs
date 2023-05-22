using System;
using UnityEngine;

namespace ZombieGame.Scripts.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class CollisionObserver : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            CollisionEnter?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            CollisionExit?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            CollisionStay?.Invoke(other);
        }

        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionStay;
        public event Action<Collision> CollisionExit;
    }
}