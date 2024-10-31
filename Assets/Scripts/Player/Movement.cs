using System;
using Codice.Client.BaseCommands;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum MovementStates
{
    Walk,
    Run,
    NoStamina
}

public class Movement : MonoBehaviour
{
    [Header("Player Infos")]
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] Camera _cam;

    
    [Header("Event at walk")]
    //Event for movement : Animation
    [SerializeField] private UnityEvent OnStartMovement; 
    [SerializeField] private UnityEvent OnEndMovement;

    [Header("Event at Run")]
    //Event for Run : Animation
    [SerializeField] private UnityEvent OnRun;
    [SerializeField] private UnityEvent OnEndRun;
    
    [Header("Player Movement Settings")]
    [SerializeField, MinValue(0)] private float _walkSpeed;
    [SerializeField, MinValue(0)] private float _runSpeed;
    [SerializeField, MinValue(0)] private float _lowStaminaSpeed;
    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public float LowStaminaSpeed { get => _lowStaminaSpeed; }
    
    
    //Private field
    private float _currentSpeed;
    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    
    private MovementStates _currentMovementState;
    public MovementStates CurrentMovementStates { get => _currentMovementState; set => _currentMovementState = value; }
    
    private bool _canMove = true;
    
    private bool _isMoving;
    public bool IsMoving => _isMoving;
    
    
    private void Start()
    {
        InitSpeed();
    }

    void Update()
    {
        UpdateCurrentSpeed();
        UpdateStamina();
    }

    private void FixedUpdate()
    {
        if(_canMove)
            ApplySpeed();
    }

    void InitSpeed() => _currentSpeed = _walkSpeed;

    
    public void ApplySpeed()
    {
        //Calcul direction
        Vector3 dir = Vector3.zero;
        dir = _cam.transform.forward * _player.MoveInput.z + _cam.transform.right * _player.MoveInput.x;
        dir.Normalize();
        dir.y = 0;
        
        //Move player
        Vector3 velocity = dir * _currentSpeed;
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        transform.LookAt(transform.position + dir);

        //Animation
        if (velocity != Vector3.zero)
        {
            OnStartMovement?.Invoke(); 
            _isMoving = true;
        }
        else
        {
            OnEndMovement?.Invoke();
            _isMoving = false;
        }
        
    }
    
    void UpdateCurrentSpeed()
    {
        switch (_currentMovementState)
        {
            case(MovementStates.Walk):
                _currentSpeed = _walkSpeed;
                break;
            case(MovementStates.Run):
                _currentSpeed = _runSpeed;
                break;
            case(MovementStates.NoStamina):
                _currentSpeed = _lowStaminaSpeed;
                break;
        }
    }
    
    void UpdateStamina()
    {
        if (_player.MoveInput != Vector3.zero && _currentMovementState == MovementStates.Run)
        {
            _player.StaminaController.DecreaseStamina(_player.StaminaController.DecreaseStaminaInTime * Time.deltaTime);
            OnRun?.Invoke();
        }
        else
        {
            _player.StaminaController.IncreaseStamina(_player.StaminaController.IncreaseStaminaInTime * Time.deltaTime);
            OnEndRun?.Invoke();
        }
    }

    public void CanMove()
    {
        _canMove = !_canMove;
    }
    
    #region Inspector test

    private void Reset()
    {
        _player = GetComponent<Player>();
        if(_player == null)
            Debug.LogError("Component 'Player' needed in 'Health' component");
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            Debug.LogError("Component 'Rigidbody' needed in 'Movement' Component");
    }

    #endregion
}
