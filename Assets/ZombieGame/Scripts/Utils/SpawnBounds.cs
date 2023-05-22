using UnityEngine;

namespace ZombieGame.Scripts.Utils
{
    public class SpawnBounds
    {
        private readonly Vector3 _bottomLeftPosition;
        private readonly Vector3 _topRightPosition;

        public SpawnBounds(Vector3 bottomLeftPosition, Vector3 topRightPosition)
        {
            _bottomLeftPosition = bottomLeftPosition;
            _topRightPosition = topRightPosition;
        }

        public bool IsInRange(Vector3 position)
        {
            return position.x >= _bottomLeftPosition.x && position.x <= _topRightPosition.x &&
                   position.z >= _bottomLeftPosition.z &&
                   position.z <= _topRightPosition.z;
        }
    }
}