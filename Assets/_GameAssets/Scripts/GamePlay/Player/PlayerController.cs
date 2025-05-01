using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    [Header("Movement Speed")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;
    private Rigidbody _playerRigidbody;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    private Vector3 _movementDirection;
    private bool _canJump = true;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Ground Settings")]
    [SerializeField]private float _playerHighth;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private StateController _stateController;

    private float _horizontalInput,_verticalInput;
    private bool _isSliding = false;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
       SetInputs();   
       SetStates();
       SetPlayerDrag();
    }

    void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }else if(Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
        else if(Input.GetKey(_jumpKey)&& _canJump && IsGrounded()){
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }

    private void SetStates(){
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var currentState = _stateController.GetCurrentState();
        var isSliding = IsSliding();

        var newState = currentState switch {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };
        if(newState != currentState){
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement(){
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };
        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed* forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerJumping(){
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up* _jumpForce, ForceMode.Impulse);
    }

    private void SetPlayerDrag(){
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };
    }

    private void ResetJump(){
        _canJump = true;
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position, Vector3.down,_playerHighth*0.5f+0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection(){
        return _movementDirection.normalized;
    }
    private bool IsSliding(){
        return _isSliding;
    }
}
