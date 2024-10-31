using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowSlider : MonoBehaviour
{
    [SerializeField] private Health _healtController = null;
    [SerializeField] private Stamina _staminaController = null;
    [SerializeField] private Slider _healthSlider = null;
    [SerializeField] private Slider _staminaSlider = null;

    private void Start()
    {
        if (_healtController == null) return;
        _healtController.OnHealthUpdate += UpdateHealthSlider;
        if (_staminaController == null) return;
        _staminaController.OnStaminaUpdate += UpdateStaminaSlider;
    }

    private void OnDestroy()
    {
        if (_healtController == null) return;
        _healtController.OnHealthUpdate -= UpdateHealthSlider;
        if (_staminaController == null) return;
        _staminaController.OnStaminaUpdate -= UpdateStaminaSlider;
    }

    public void UpdateHealthSlider(int currentLife)
    {
        if (_healtController == null) return;
        _healthSlider.value = currentLife;
    }

    public void UpdateStaminaSlider(float currentStamina)
    {
        if (_staminaController == null) return;
        _staminaSlider.value = currentStamina;
    }
}
