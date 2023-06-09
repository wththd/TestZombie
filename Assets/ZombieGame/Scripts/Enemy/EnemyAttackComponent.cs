﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Observers;

namespace ZombieGame.Scripts.Enemy
{
    public class EnemyAttackComponent : MonoBehaviour
    {
        [SerializeField] private float range;

        [SerializeField] private int damage;

        [SerializeField] private float cooldown;

        [SerializeField] private List<TriggerObserver> observers;

        [SerializeField] private EnemyAnimator enemyAnimator;

        private bool isAttacking;

        private bool isOnCooldown;
        public float DistanceToTarget { get; set; }

        private bool CanAttack => !isOnCooldown && DistanceToTarget < range;

        private void Awake()
        {
            foreach (var observer in observers) observer.TriggerEnter += OnTriggerEnter;
        }

        private void Update()
        {
            AttackUpdate();
        }

        private void OnEnable()
        {
            StartCoroutine(CooldownRoutine());
        }

        private void OnTriggerEnter(Collider triggerCollider)
        {
            if (!isAttacking) return;

            if (triggerCollider.CompareTag("Player"))
                triggerCollider.gameObject.GetComponent<IHealth>().TakeDamage(damage, DamageType.Melee);
        }

        private void AttackUpdate()
        {
            if (!CanAttack) return;

            StartAttack();
        }

        private void StartAttack()
        {
            isAttacking = true;
            enemyAnimator.PlayAttack();
            StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(cooldown);
            isOnCooldown = false;
            isAttacking = false;
        }
    }
}