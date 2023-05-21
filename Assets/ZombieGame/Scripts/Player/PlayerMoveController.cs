using UnityEngine;
using Zenject;
using ZombieGame.Scripts.Services;

public class PlayerMoveController : MonoBehaviour
{
    private IInputService _inputService;
    private CharacterController _characterController;
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float aimSpeed;
    [SerializeField] 
    private Camera playerCamera;

    private bool isDead;

    [Inject]
    private void Inject(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        
        MoveUpdate();
        RotateUpdate();
    }

    private void RotateUpdate()
    {
        var screenPointPosition = _inputService.PointPosition;
        var position = transform.position;
        screenPointPosition.z = Vector3.Distance(playerCamera.transform.position, position);
        var pointPosition = playerCamera.ScreenToWorldPoint(screenPointPosition);
        pointPosition.y = position.y;
        transform.LookAt(pointPosition, Vector3.up);
    }

    private void MoveUpdate()
    {
        var movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > double.Epsilon)
        {
            movementVector = playerCamera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.Normalize();
        }

        movementVector += Physics.gravity;
        var targetSpeed = _inputService.IsAttackButton() ? aimSpeed : speed;

        _characterController.Move(movementVector * (targetSpeed * Time.deltaTime));
    }
    
    public void OnHit()
    {
        
    }
    
    public void OnDeath()
    {
        isDead = true;
    }

    public class Factory : PlaceholderFactory<PlayerMoveController>
    {
    }
}
