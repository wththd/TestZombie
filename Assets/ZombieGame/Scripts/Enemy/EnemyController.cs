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
        public event Action<EnemyController> Died;
        
        private IMemoryPool _pool;
        private float _targetDistance;
        private Transform _target;
        private NavMeshAgent agent;
        private Vector3 targetPosition;
        private EnemyAnimator enemyAnimator;
        private Collider enemyCollider;

        private EnemyDeathComponent enemyDeathComponent;
        private EnemyAttackComponent enemyAttackComponent;

        public bool CanDie { get; private set; }
        public bool CanAttack { get; private set; }
        private EnemyDeathComponent EnemyDeathComponent => enemyDeathComponent ??= GetComponent<EnemyDeathComponent>();
        private EnemyAttackComponent EnemyAttackComponent => enemyAttackComponent ??= GetComponent<EnemyAttackComponent>();
        private float distanceToTarget;
        private bool isDead;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyCollider = agent.GetComponent<Collider>();
        }

        private void OnDie()
        {
            isDead = true;
            Died?.Invoke(this);
        }

        private void Update()
        {
            if (isDead)
            {
                return;
            }
            
            MoveUpdate();
            AttackUpdate();
        }

        private void AttackUpdate()
        {
            if (CanAttack)
            {
                EnemyAttackComponent.DistanceToTarget = distanceToTarget;
            }
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
            if (CanDie)
            {
                EnemyDeathComponent.Died += OnDie;
            }
        }

        public void OnDespawned()
        {
            _pool = null;
            
            if (CanDie)
            {
                EnemyDeathComponent.Died -= OnDie;
            }

            isDead = false;
        }

        public void Dispose()
        {
            agent.isStopped = true;
            enemyCollider.enabled = false;

            StopAllCoroutines();
            StartCoroutine(DeSpawnRoutine());
        }

        public void OnHit()
        {
            agent.velocity *= 0.9f;
        }

        private IEnumerator DeSpawnRoutine()
        {
            while (!enemyAnimator.IsDead)
            {
                yield return null;
            }

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