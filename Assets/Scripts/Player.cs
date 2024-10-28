using System;using System.Runtime.CompilerServices;
using Codice.CM.SEIDInfo;
using NaughtyAttributes;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Movement),typeof(Health), typeof(Stamina))]
[RequireComponent(typeof(Jump))]
public class Player : MonoBehaviour
{
    [Header("Player Component")]
    [SerializeField, Required, Label("Movement")] private Movement _movementController;
    [SerializeField, Required, Label("Health")] private Health _healthController;
    [SerializeField, Required, Label("Stamina")] private Stamina _staminaController;
    [SerializeField, Required, Label("Jump")] private Jump _JumpController;
    
    
    [Header("Player Infos")] 
    private MovementStates _currentMovementState;
    public MovementStates CurrentMovementStates { get => _currentMovementState; set => _currentMovementState = value; }
    
    [HorizontalLine(color : EColor.Red)]
    public bool showLife;
    [ShowIf("showLife"),BoxGroup("Life"),ProgressBar("Health", "_maxHealth", EColor.Red),SerializeField]
    private int _currentLife;
    public int CurrentLife { get => _currentLife; set => _currentLife = value; }
    private int _maxHealth;
    
    [HorizontalLine(color : EColor.Blue)]
    public bool showStamina;
    [ShowIf("showStamina"),BoxGroup("Stamina"), ProgressBar("Stamina", "_maxStamina", EColor.Blue), SerializeField]
    private float _currentStamina;
    public float CurrentStamina { get => _currentStamina; }
    private float _maxStamina;

    [HorizontalLine(color : EColor.Green)]
    public bool showSpeed;
    [ShowIf("showSpeed"), BoxGroup("Speed"), SerializeField]
    private float _currentSpeed;
    public float CurrentSpeed { get => _currentSpeed; }

    [HorizontalLine(color: EColor.Black)] 
    public bool showJump;
    [ShowIf("showJump"), BoxGroup("Jump"), SerializeField]
    private float _currentJumpForce;
    public float CurrentJumpForce { get => _currentJumpForce; }


    void Awake()
    {
        Reset();
        _maxStamina = _staminaController.MaxStamina;
        _maxHealth = _healthController.MaxHealth;
    }
    
    private void Start()
    {
        //Init Life;
        _currentLife = _healthController.MaxHealth;
        
        //Init Stamina
        _currentStamina = _staminaController.InitStamina();
        
        //Init Speed
        _currentSpeed = _movementController.WalkSpeed;
        _currentMovementState = MovementStates.Walk;
        
        //Init JumpForce
        _currentJumpForce = _JumpController.InitJumpForce();
        
        //Inspector bool;
        showStamina = true;
        showLife = true;
        showSpeed = true;
        showJump = true;

    }

    private void Update()
    {
        MonVector vector = new MonVector(3, 4, 5);
        _healthController.Die();
        UpdateStamina();
        UpdateMovementState();
        UpdateCurrentSpeed();
    }

    private void FixedUpdate()
    {
        _movementController.ApplySpeed(_currentSpeed);
    }

    void UpdateCurrentSpeed()
    {
        switch (_currentMovementState)
        {
            case(MovementStates.Walk):
                _currentSpeed = _movementController.WalkSpeed;
                break;
            case(MovementStates.Run):
                _currentSpeed = _movementController.RunSpeed;
                break;
            case(MovementStates.NoStamina):
                _currentSpeed = _movementController.LowStaminaSpeed;
                break;
        }
    }

    void UpdateMovementState()
    {
        if (_movementController.IsRunning)
        {
            _currentMovementState = MovementStates.Run;
        }
        else if (_currentStamina < 5)
        {
            _currentMovementState = MovementStates.NoStamina;
        }
        else
        {
            _currentMovementState = MovementStates.Walk;
        }
    }

    void UpdateStamina()
    {
        if (_movementController.MoveInput != Vector3.zero && _currentMovementState == MovementStates.Run)
        {
            _currentStamina = _staminaController.DecreaseStamina(_staminaController.DecreaseStaminaInTime * Time.deltaTime);
        }
        else
        {
            _currentStamina = _staminaController.IncreaseStamina(_staminaController.IncreaseStaminaInTime * Time.deltaTime);
        }
    }
    

    #region Inspector Button
    #region Life
    [ShowIf("showLife"), Button("Health : Get")]
    void GetHealth() => Debug.Log(_healthController.GetHealth());

    [ShowIf("showLife"), Button("Health : Regeneration")]
    void RegenerationButton() => _currentLife = _healthController.Regeneration(10);
    [ShowIf("showLife"), Button("Health : TakeDamage")]
    void TakeDamageButton() => _currentLife = _healthController.TakeDamage(10);
    #endregion
    
    #region Stamina
    [ShowIf("showStamina"), Button("Stamina : Get")]
    void GetStamina() => _staminaController.GetStamina();

    [ShowIf("showStamina"), Button("Stamina : Increase")]
    void Increase() => _currentStamina = _staminaController.IncreaseStamina(10);
    

    [ShowIf("showStamina"), Button("Stamina : Decrease")]
    void Decrease() => _currentStamina = _staminaController.DecreaseStamina(10);
    
    #endregion
    
    #region CurrentSpeed
    [ShowIf("showSpeed"), Button("Speed : Get")]
    void GetSpeed() => _movementController.GetCurrentSpeed();
    #endregion
    #endregion
        
    #region Validate Inspector
    void Reset()
    {
        _movementController = GetComponent<Movement>();
        _healthController = GetComponent<Health>();
        _staminaController = GetComponent<Stamina>();
        _JumpController = GetComponent<Jump>();
        if(_movementController == null)
            Debug.LogError("Component 'Movement' needed");
        if(_healthController == null)
            Debug.LogError("Component 'Health' needed");
        if(_staminaController == null)
            Debug.LogError("Component 'Stamina' needed");
        if(_JumpController == null)
            Debug.LogError("Component 'Jump' needed");
    }
    #endregion
}
