using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowSlider : MonoBehaviour
{
    [SerializeField] private Health _healtController;
    [SerializeField] private Stamina _staminaController;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _staminaSlider;

    private void Start()
    {
        _healtController.OnHealthUpdate += UpdateHealthSlider; 
        _staminaController.OnStaminaUpdate += UpdateStaminaSlider;
    }

    private void OnDestroy()
    {
        _healtController.OnHealthUpdate -= UpdateHealthSlider;
        _staminaController.OnStaminaUpdate -= UpdateStaminaSlider;
    }

    public void UpdateHealthSlider(int currentLife)
    {
        _healthSlider.value = currentLife;
    }

    public void UpdateStaminaSlider(float currentLife)
    {
        _staminaSlider.value = currentLife;
    }
}
