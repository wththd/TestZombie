using System;
using UnityEngine;

namespace ZombieGame.Scripts.Observers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
    }
}