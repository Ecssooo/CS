using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Player Infos")]
    [SerializeField] private Player _player;
    
    [Header("Health Settings")]
    [SerializeField, MinValue(0)] int _maxHealth;
    public int MaxHealth { get => _maxHealth;}

    [SerializeField] private UnityEvent _onDie;
    public event Action<int> OnHealthUpdate;

  
    #region Health Method
    
    public int GetHealth() => _player.CurrentLife;
    
    public int Regeneration(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't heal");
            return GetHealth();
        }
        int currentLife = GetHealth();
        if (currentLife + value > _maxHealth)
            return _maxHealth;
        OnHealthUpdate.Invoke(currentLife + value);
        return currentLife + value;
    }
    
    public int TakeDamage(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't make damage");
            return GetHealth();
        }
        int currentLife = GetHealth();
        if (currentLife - value < 0)
            return 0;
        OnHealthUpdate.Invoke(currentLife - value);
        return currentLife - value;
    }

    public void Die()
    {
        if (GetHealth() <= 0)
        {
            _onDie.Invoke();
            Destroy(gameObject, 2f);
        }
    }
    #endregion
    
    
    #region Inspector test

    private void Reset()
    {
        _player = GetComponent<Player>();
        if(_player == null)
            Debug.LogError("Component 'Player' needed in 'Health' component");
    }

    #endregion
}