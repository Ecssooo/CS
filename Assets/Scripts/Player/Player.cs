using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement),typeof(Health), typeof(Stamina))]
public class Player : MonoBehaviour
{
    [Header("Player Component")]
    [SerializeField, Required, Label("Movement")] private Movement _movementController;
    [SerializeField, Required, Label("Health")] private Health _healthController;
    [SerializeField, Required, Label("Stamina")] private Stamina _staminaController;
    
    public Movement MovementController { get => _movementController; }

    public Health HealthController { get => _healthController; }

    public Stamina StaminaController { get => _staminaController; }
    
    [Header("Input")]
    [SerializeField, Required, Label("Move Input")] InputActionReference moveAction;
    [SerializeField, Required, Label("Run Input")] InputActionReference runAction;
    
    //Private field
    [ShowNonSerializedField] private Vector3 _moveInput;
    public Vector3 MoveInput { get => _moveInput; }
    
    
    #region Player Input
    
    void Start()
    {
        moveAction.action.started += MoveStart;
        moveAction.action.performed += MovePerformed;
        moveAction.action.canceled += MoveCanceled;
        runAction.action.performed += RunPerformed;
        runAction.action.canceled += RunCancelled;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        moveAction.action.started -= MoveStart;
        moveAction.action.performed -= MovePerformed;
        moveAction.action.canceled -= MoveCanceled;
        runAction.action.performed -= RunPerformed;
        runAction.action.canceled -= RunCancelled;
    }

    void MoveStart(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector3>();
    void MovePerformed(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector3>();
    void MoveCanceled(InputAction.CallbackContext context) => _moveInput = Vector3.zero;
    void RunPerformed(InputAction.CallbackContext context) => _movementController.CurrentMovementStates = MovementStates.Run;
    void RunCancelled(InputAction.CallbackContext context) => _movementController.CurrentMovementStates = MovementStates.Walk;
    #endregion
    
    
    #region Validate Inspector
    void Reset()
    {
        _movementController = GetComponent<Movement>();
        _healthController = GetComponent<Health>();
        _staminaController = GetComponent<Stamina>();
        if(_movementController == null)
            Debug.LogError("Component 'Movement' needed");
        if(_healthController == null)
            Debug.LogError("Component 'Health' needed");
        if(_staminaController == null)
            Debug.LogError("Component 'Stamina' needed");
    }
    #endregion

}
