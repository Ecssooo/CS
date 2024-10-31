using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


public class Stamina : MonoBehaviour
{
    
    [Header("Stamina Settings")]
    [SerializeField, MinValue(0)] private float _maxStamina;
    public float MaxStamina { get => _maxStamina; }
    [SerializeField, MinValue(0)] private float _decreaseStaminaInTime;
    [SerializeField, MinValue(0)] private float _IncreaseStaminaInTime;
    public float DecreaseStaminaInTime { get => _decreaseStaminaInTime; }
    public float IncreaseStaminaInTime { get => _IncreaseStaminaInTime; }
    
    
    //Private field
    private float _currentStamina;
    public float CurrentStamina { get => _currentStamina; set => _currentStamina = value; }
    public event Action<float> OnStaminaUpdate;
    
    private void Start()
    {
        InitStamina();
    }
    
    #region Stamina Method

    public void InitStamina() => _currentStamina = _maxStamina;
    
    public void IncreaseStamina(float value)
    {
        if (value < 0)
        {
            Debug.Log("Negative value can't regenerate stamina");
        }

        _currentStamina += value;
        if (_currentStamina > _maxStamina)
            _currentStamina = _maxStamina;
        OnStaminaUpdate?.Invoke(_currentStamina);
    }

    public void DecreaseStamina(float value)
    {
        if (value < 0)
        {
            Debug.Log("Negative value can't use stamina");
        }
        _currentStamina -= value;
        if (_currentStamina < 0)
            _currentStamina = 0;
        OnStaminaUpdate?.Invoke(_currentStamina);
    }
    #endregion
    
    #region Inspector test
    
    #endregion
}
