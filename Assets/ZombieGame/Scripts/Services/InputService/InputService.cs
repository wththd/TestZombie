using UnityEngine;

namespace ZombieGame.Scripts.Services.InputService
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        public abstract Vector2 Axis { get; }
        public abstract Vector3 PointPosition { get; }
        public abstract bool IsAttackButton();
        public abstract bool IsReloadButtonDown();
        public abstract bool IsJumpButtonDown();
    }
}