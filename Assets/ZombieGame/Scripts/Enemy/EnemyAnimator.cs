using UnityEngine;
using UnityEngine.AI;
using ZombieGame.Scripts.Observers;

namespace ZombieGame.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private static readonly int Attack = Animator.StringToHash("Attack");

        [HideInInspector] public bool IsDead;

        private Animator enemyAnimator;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            enemyAnimator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            enemyAnimator.SetFloat(Speed, navMeshAgent.velocity.magnitude);
        }

        public void EnteredState(int stateHash)
        {
            if (stateHash == Idle) IsDead = false;
        }

        public void ExitedState(int stateHash)
        {
            if (stateHash == Death) IsDead = true;
        }

        public void PlayHit()
        {
            enemyAnimator.SetTrigger(Hit);
        }

        public void PlayDeath()
        {
            enemyAnimator.SetTrigger(Death);
        }

        public void PlayAttack()
        {
            enemyAnimator.SetTrigger(Attack);
        }
    }
}