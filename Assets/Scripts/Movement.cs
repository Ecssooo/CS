using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
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
    
    
    [Header("Player Movement Settings")]
    [SerializeField, MinValue(0)] private float _walkSpeed;
    [SerializeField, MinValue(0)] private float _runSpeed;
    [SerializeField, MinValue(0)] private float _lowStaminaSpeed;
    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public float LowStaminaSpeed { get => _lowStaminaSpeed; }
    
    [Header("Input")] 
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference runAction;
    [ShowNonSerializedField] private Vector3 _moveInput;
    public Vector3 MoveInput { get => _moveInput; }
    
    private bool _isRunning;
    public bool IsRunning { get { return _isRunning; } }

    void Start()
    {
        moveAction.action.performed += MovePerformed;
        moveAction.action.canceled += MoveCanceled;
        runAction.action.performed += RunPerformed;
        runAction.action.canceled += RunCancelled;
    }

    private void OnDestroy()
    {
        moveAction.action.performed -= MovePerformed;
        moveAction.action.canceled -= MoveCanceled;
        runAction.action.performed -= RunPerformed;
        runAction.action.canceled -= RunCancelled;
    }

    
    void MovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector3>();

        Vector3 moveX = transform.right * _moveInput.x;
        Vector3 moveZ = transform.forward * _moveInput.z;

        _moveInput = (moveX + moveZ).normalized;
    }

    void MoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector3.zero;
    }
    
    void RunPerformed(InputAction.CallbackContext context) => _isRunning = true;

    void RunCancelled(InputAction.CallbackContext context) => _isRunning = false;
    
    public void ApplySpeed(float speed)
    {
        Vector3 velocity = speed * _moveInput;
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    
    #region CurrentSpeed Method

    public float GetCurrentSpeed() => _player.CurrentSpeed;
    
    
    #endregion
    
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
