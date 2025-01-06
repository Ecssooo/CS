using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Player Infos")]
    [BoxGroup("Life"),ProgressBar("Health", "_maxHealth", EColor.Red),SerializeField]
    private int _currentHealth;

    private bool _isDead;
    
    //Initial Parameters
    [Header("Health Settings")]
    [SerializeField, MinValue(0)] int _maxHealth;
    [SerializeField] private GameObject _parentToDestroy;
    
    //Events
    [SerializeField] private UnityEvent OnDie;
    [SerializeField] private UnityEvent OnTakeDamage;
    [SerializeField] private UnityEvent OnRegeneration;
    public event Action<int> OnHealthUpdate;
    
    //Property
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHealth { get => _maxHealth;}
    public bool IsDead { get => _isDead; }
  
    
    private void Start()
    {
        InitCurrentHealth();
    }

    private void Update()
    {
        CheckIfDead();
    }

    
    #region Health Method
    
    private void InitCurrentHealth() => _currentHealth = _maxHealth;

    /// <summary>
    /// Increase _currentHealth
    /// </summary>
    /// <param name="value">HP to add</param>
    public void Regeneration(int value)
    {
        if (value < 0) { throw new ArgumentException("value must be positive", "value"); }
        
        _currentHealth += value;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        } 
        OnHealthUpdate?.Invoke(_currentHealth);
        OnRegeneration?.Invoke();
    }
    
    /// <summary>
    /// Decrease _currentHealth
    /// </summary>
    /// <param name="value">HP to remove</param>
    public void TakeDamage(int value)
    {
        if (value < 0) throw new ArgumentException("value must be positive", "value");
        
        _currentHealth -= value;
        if (_currentHealth < 0)
        {
            OnDie?.Invoke();
            _currentHealth = 0;
        }
        OnHealthUpdate?.Invoke(_currentHealth);
        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// Check if player is dead
    /// Y : Invoke Event, Destroy GameObject
    /// </summary>
    public bool CheckIfDead()
    {
        if (_currentHealth <= 0)
        {
            StartCoroutine(Die());
            IEnumerator Die()
            {
                _isDead = true;
                OnDie?.Invoke();
                
                yield return new WaitForSeconds(1f);
                Destroy(_parentToDestroy);
            }
        }
        return _isDead;
    }
    
    #endregion

    public void InitComponent(int maxHealth)
    {
        _maxHealth = maxHealth;
        InitCurrentHealth();
    }
}