using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using ZombieGame.Scripts.Controllers;
using ZombieGame.Scripts.Damage;
using ZombieGame.Scripts.Services;

namespace ZombieGame.Scripts.Player
{
    [RequireComponent(typeof(PlayerAnimationController), typeof(CharacterController))]
    public class PlayerFire : MonoBehaviour
    {
        private IInputService _inputService;
        private Projectile.Factory _projectileFactory;

        private bool isCurrentlyAttacking;
        private bool isOnCooldown;
        private PlayerAnimationController playerAnimationController;
        private CharacterController characterController;
        private IWeapon weapon;
        private bool isDead;

        [Inject]
        private void Inject(IInputService inputService, Projectile.Factory projectileFactory)
        {
            _inputService = inputService;
            _projectileFactory = projectileFactory;
        }

        private void Awake()
        {
            playerAnimationController = GetComponent<PlayerAnimationController>();
            characterController = GetComponent<CharacterController>();
            weapon = GetComponent<IWeapon>();

            Assert.IsNotNull(weapon);
        }

        private void Update()
        {
            if (isDead)
            {
                return;
            }

            UpdateAttackState();
            UpdateFireState();
        }

        private void UpdateAttackState()
        {
            var isAttack = _inputService.IsAttackButton();
            if (isCurrentlyAttacking != isAttack)
            {
                playerAnimationController.SetAimState(isAttack);
                isCurrentlyAttacking = isAttack;
            }
        }

        private void UpdateFireState()
        {
            if (isCurrentlyAttacking && !isOnCooldown && weapon.CanFire())
            {
                SpawnProjectile();
                StartCoroutine(CooldownRoutine());
            }
        }

        private void SpawnProjectile()
        {
            _projectileFactory.Create(weapon.FirePoint.position, characterController.transform.forward,
                weapon.WeaponSettings);
            weapon.Fire();
            playerAnimationController.TriggerShotAnimation();
        }

        private IEnumerator CooldownRoutine()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(weapon.Cooldown);
            isOnCooldown = false;
        }

        public void OnDeath()
        {
            isCurrentlyAttacking = false;
            isDead = true;
        }
    }
}