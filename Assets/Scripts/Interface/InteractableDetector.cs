using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetector : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactInput;

    private bool _interact;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IIteraction interactable))
        {
            if (interactable.InputState == InputStates.NoInput)
            {
                interactable.StartInteraction(this);  
            }
            else
            {
                if (_interact)
                {
                    interactable.StartInteraction(this);
                    if (interactable.InputState == InputStates.InputStarted) _interact = false;
                }
                else if(!_interact && interactable.InputState == InputStates.InputPerformed)
                {
                    interactable.EndInteraction(this);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IIteraction interactable))
        {
            interactable.EndInteraction(this);
        }
    }

    void InteractInputTrigger(InputAction.CallbackContext context)
    {
        _interact = true;
    }

    void InteractInputCanceled(InputAction.CallbackContext context)
    {
        _interact = false;
    }
    
    private void Start()
    {
        _interactInput.action.started += InteractInputTrigger;
        _interactInput.action.canceled += InteractInputCanceled;
    }

    private void OnDestroy()
    {
        _interactInput.action.started -= InteractInputTrigger;
        _interactInput.action.canceled -= InteractInputCanceled;
    }
}
