using System;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour, IIteraction
{
    [SerializeField] private InputStates _inputStates;
    
    public InputStates InputState { get; set; }
    [SerializeField] private UnityEvent OnPressure;
    [SerializeField] private UnityEvent OnEndPressure;
    
    private void Start()
    {
        InputState = _inputStates;
    }

    public void StartInteraction(InteractableDetector id)
    {
        OnPressure?.Invoke();
    }

    public void EndInteraction(InteractableDetector id)
    {
        OnEndPressure?.Invoke();
    }
}
