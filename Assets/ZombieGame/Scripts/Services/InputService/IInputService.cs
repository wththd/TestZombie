using UnityEngine;

namespace ZombieGame.Scripts.Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        Vector3 PointPosition { get; }

        bool IsAttackButton();
        bool IsReloadButtonDown();
        bool IsJumpButtonDown();
    }
}