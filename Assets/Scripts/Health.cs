using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    
    [Header("Health Settings")]
    [SerializeField, MinValue(0)] int _maxHealth;
    public int MaxHealth { get => _maxHealth;}

    [ShowNonSerializedField, MinValue(0)] private int _currentHealth;
    public int CurrentHealth { get => _maxHealth; set => _currentHealth = value; }
    
    [SerializeField] private UnityEvent _onDie;
    
  
    #region Health Method
    
    
    public int Regeneration(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't heal");
            return _currentHealth;
        }
        int currentLife = _currentHealth;
        if (currentLife + value > _maxHealth)
            return _maxHealth;
        return currentLife + value;
    }
    
    public int TakeDamage(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't make damage");
            return _currentHealth;
        }
        int currentLife = _currentHealth;
        if (currentLife - value < 0)
            return 0;
        return currentLife - value;
    }

    public void Die()
    {
        if (_currentHealth <= 0)
        {
            _onDie.Invoke();
            Destroy(gameObject, 2f);
        }
    }
    #endregion
    
    
    #region Inspector test
    

    #endregion
}
