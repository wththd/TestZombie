using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ZombieGame.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour, IPoolable<Vector3, Transform, float, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private Transform _target;
        private float _targetDistance;
        private NavMeshAgent agent;
        private float distanceToTarget;
        private EnemyAnimator enemyAnimator;
        private EnemyAttackComponent enemyAttackComponent;
        private Collider enemyCollider;

        private EnemyDeathComponent enemyDeathComponent;
        private bool isDead;
        private Vector3 targetPosition;

        public bool CanDie { get; private set; }
        public bool CanAttack { get; private set; }
        private EnemyDeathComponent EnemyDeathComponent => enemyDeathComponent ??= GetComponent<EnemyDeathComponent>();

        private EnemyAttackComponent EnemyAttackComponent =>
            enemyAttackComponent ??= GetComponent<EnemyAttackComponent>();


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyCollider = agent.GetComponent<Collider>();
        }

        private void Update()
        {
            if (isDead) return;

            MoveUpdate();
            AttackUpdate();
        }

        public void Dispose()
        {
            agent.isStopped = true;
            enemyCollider.enabled = false;

            StopAllCoroutines();
            StartCoroutine(DeSpawnRoutine());
        }

        public void OnSpawned(Vector3 position, Transform target, float targetDistance, IMemoryPool pool)
        {
            transform.position = position;
            _target = target;
            _targetDistance = targetDistance;
            _pool = pool;

            agent.isStopped = false;
            enemyCollider.enabled = true;

            CanDie = EnemyDeathComponent != null;
            CanAttack = EnemyAttackComponent != null;
            if (CanDie) EnemyDeathComponent.Died += OnDie;
        }

        public void OnDespawned()
        {
            _pool = null;

            if (CanDie) EnemyDeathComponent.Died -= OnDie;

            isDead = false;
        }

        public event Action<EnemyController> Died;

        private void OnDie()
        {
            isDead = true;
            Died?.Invoke(this);
        }

        private void AttackUpdate()
        {
            if (CanAttack) EnemyAttackComponent.DistanceToTarget = distanceToTarget;
        }

        private void MoveUpdate()
        {
            if (_target != null && AgentNotReached())
            {
                agent.destination = _target.position;
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }

        public void OnHit()
        {
            agent.velocity *= 0.9f;
        }

        private IEnumerator DeSpawnRoutine()
        {
            while (!enemyAnimator.IsDead) yield return null;

            Despawn();
        }

        public void Despawn()
        {
            _pool.Despawn(this);
        }

        private bool AgentNotReached()
        {
            distanceToTarget = Vector3.Distance(agent.transform.position, _target.position);
            return distanceToTarget >= _targetDistance;
        }

        public class Factory : PlaceholderFactory<Vector3, Transform, float, EnemyController>
        {
        }
    }
}