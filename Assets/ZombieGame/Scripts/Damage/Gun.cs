using System;
using UnityEngine;

namespace ZombieGame.Scripts.Damage
{
    public class Gun : Weapon
    {
        public ProjectileSettings WeaponSettings { get; }
        public float Cooldown { get; }
        public int Capacity { get; }
    }
}