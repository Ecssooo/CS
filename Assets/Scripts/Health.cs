using Codice.Client.BaseCommands;
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
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    // [SerializeField] private UnityEvent _onDie;
    public event UnityAction OnTakeDamage;
    
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
    
    public void Regeneration(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't heal");
            return;
        }
        if (_currentHealth + value > _maxHealth)
            _currentHealth = _maxHealth;
        _currentHealth += value;
        OnTakeDamage.Invoke();
    }
    
    public void TakeDamage(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("Negative value can't make damage");
            return;
        }
        if (_currentHealth - value < 0)
            _currentHealth = 0;
        _currentHealth -= value;
        OnTakeDamage.Invoke();
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
    void TakeDamage() => TakeDamage(10);
    [Button, ShowIf("showLife")]
    void Regeneration() =>  Regeneration(10);

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
