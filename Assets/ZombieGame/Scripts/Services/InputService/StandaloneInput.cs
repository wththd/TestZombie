using UnityEngine;

namespace ZombieGame.Scripts.Services
{
    public class StandaloneInput : InputService
    {
        public override Vector2 Axis => GetStandaloneAxis();
        public override Vector3 PointPosition => GetStandalonePointPosition();

        public override bool IsAttackButton()
        {
            return Input.GetMouseButton(0);
        }

        public override bool IsReloadButtonDown()
        {
            return Input.GetKeyDown(KeyCode.R);
        }

        public override bool IsJumpButtonDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        private static Vector2 GetStandaloneAxis()
        {
            return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
        }

        private static Vector3 GetStandalonePointPosition()
        {
            return Input.mousePosition;
        }
    }
}