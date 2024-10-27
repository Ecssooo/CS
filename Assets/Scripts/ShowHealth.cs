using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _health.OnTakeDamage += UpdateSlider;
    }

    public void UpdateSlider()
    {
        _slider.value = _health.CurrentHealth;
    }
}
