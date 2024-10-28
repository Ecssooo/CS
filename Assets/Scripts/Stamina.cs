using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


public class Stamina : MonoBehaviour
{
    [Header("Player Infos")] 
    [SerializeField] private Player _player;
    
    [Header("Stamina Settings")]
    [SerializeField, MinValue(0)] private float _maxStamina;
    public float MaxStamina { get => _maxStamina; }

    [SerializeField] private float _decreaseStaminaInTime;
    [SerializeField] private float _IncreaseStaminaInTime;
    public float DecreaseStaminaInTime { get => _decreaseStaminaInTime; }
    public float IncreaseStaminaInTime { get => _IncreaseStaminaInTime; }
    
    public event Action<float> OnStaminaUpdate;
    
    private void Start()
    {
        InitStamina();
    }
    
    
    #region Stamina Method

    public float InitStamina() => _maxStamina;
        

    public float GetStamina() => _player.CurrentStamina;
    
    public float IncreaseStamina(float value)
    {
        if (value < 0)
        {
            Debug.Log("Negative value can't regenerate stamina");
            return GetStamina();
        }

        float currentStamina = GetStamina();
        if (currentStamina + value > _maxStamina)
            return _maxStamina;
        OnStaminaUpdate.Invoke(currentStamina + value);
        return currentStamina + value;
    }

    public float DecreaseStamina(float value)
    {
        if (value < 0)
        {
            Debug.Log("Negative value can't use stamina");
            return GetStamina();
        }
        float currentStamina = GetStamina();
        if (currentStamina - value < 0)
            return 0;
        OnStaminaUpdate.Invoke(currentStamina - value);
        return currentStamina - value;
    }
    #endregion
    
    #region Inspector test

    private void Reset()
    {
        _player = GetComponent<Player>();
        if(_player == null)
            Debug.LogError("Component 'Player' needed in 'Stamina' component");
    }

    #endregion
}
