using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    
    [Header("Health Settings")]
    [SerializeField, MinValue(0)] int _maxHealth;
    public int MaxHealth { get => _maxHealth;}

    [Header("InGame Infos")]
    [SerializeField] private bool showLife;
    
    [ShowIf("showLife"), SerializeField, MinValue(0), ProgressBar("Health", "_maxHealth", EColor.Green)]
    private int _currentHealth;
    public int CurrentHealth { get => _maxHealth; set => _currentHealth = value; }
    
    // [SerializeField] private UnityEvent _onDie;

    private void Start()
    {
        InitHealth();
        UnityMeCasseLesCouilles();
    }


    #region Health Method

    void InitHealth()
    {
        _currentHealth = _maxHealth;
    }
    
    public int Regeneration(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't heal");
            return _currentHealth;
        }
        int newLife = _currentHealth;
        if (newLife + value > _maxHealth)
            return _maxHealth;
        return newLife + value;
    }
    
    public int TakeDamage(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't make damage");
            return _currentHealth;
        }
        int newLife = _currentHealth;
        if (newLife - value < 0)
            return 0;
        return newLife - value;
    }

    public void Die()
    {
        if (_currentHealth <= 0)
        {
            //_onDie.Invoke();
            Destroy(gameObject, 2f);
        }
    }
    #endregion
    
    
    #region Inspector test
    
    #region Button

    [Button, ShowIf("showLife")]
    void GetHealth() => Debug.Log(_currentHealth);
    [Button, ShowIf("showLife")]
    void TakeDamage() => _currentHealth = TakeDamage(10);
    [Button, ShowIf("showLife")]
    void Regeneration() =>  _currentHealth = Regeneration(10);

    private void UnityMeCasseLesCouilles()
    {
        if (!showLife)
        {
            showLife = true;
        }
    }
    
    #endregion

    #endregion
}
