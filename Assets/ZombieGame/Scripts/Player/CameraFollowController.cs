using UnityEngine;

namespace ZombieGame.Scripts.Player
{
    public class CameraFollowController : MonoBehaviour
    {
        [SerializeField]
        private float rotationAngleX;
        [SerializeField]
        private int distance;
        [SerializeField]
        private float offsetY;
        
        private Transform _following;

        private Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = transform;
        }

        private void LateUpdate()
        {
            if (_following == null)
            {
                return;
            }

            var rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            cameraTransform.rotation = rotation;
            cameraTransform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }
        
        private Vector3 FollowingPointPosition()
        {
            var followingPosition = _following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
        
        void OnDrawGizmosSelected()
        {
            Camera camera = GetComponent<Camera>();
            Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p, 0.1F);
        }
    }
}