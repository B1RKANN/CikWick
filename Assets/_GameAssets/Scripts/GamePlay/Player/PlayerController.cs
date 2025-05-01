using NUnit.Framework;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
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


    private float _horizontalInput,_verticalInput;
    private bool _isSliding = false;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
       SetInputs();   
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

    private void SetPlayerMovement(){
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        if(_isSliding){
        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed* _slideMultiplier, ForceMode.Force);
        }
        else{
            _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerJumping(){
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up* _jumpForce, ForceMode.Impulse);
    }

    private void SetPlayerDrag(){
        if(_isSliding){
        _playerRigidbody.linearDamping = _slideDrag;
        }
        else{
            _playerRigidbody.linearDamping = _groundDrag;
        }
    }

    private void ResetJump(){
        _canJump = true;
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position, Vector3.down,_playerHighth*0.5f+0.2f, _groundLayer);
    }
}
