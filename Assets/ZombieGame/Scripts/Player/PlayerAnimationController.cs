using UnityEngine;

namespace ZombieGame.Scripts.Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int X = Animator.StringToHash("X");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int Aiming = Animator.StringToHash("Aiming");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private Animator _animator;
        private CharacterController _characterController;

        private bool isCurrentlyAttacking;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateMoveAnimation();
        }

        private void UpdateMoveAnimation()
        {
            var velocity = _characterController.velocity;
            var horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
            var horizontalSpeed = horizontalVelocity.magnitude;
            _animator.SetFloat(Speed, horizontalSpeed, 0.1f, Time.deltaTime);
            _animator.SetFloat(X, horizontalVelocity.x);
            _animator.SetFloat(Y, horizontalVelocity.z);
        }

        public void SetAimState(bool state)
        {
            _animator.SetBool(Aiming, state);
        }

        public void TriggerShotAnimation()
        {
            _animator.SetTrigger(Shoot);
        }

        public void PlayHit()
        {
            // No hit animation on model
        }

        public void PlayDeath()
        {
            _animator.SetBool(Dead, true);
        }
    }
}