using UnityEngine;

public enum InputStates
{
    NoInput,
    InputStarted,
    InputPerformed
}


public interface IIteraction
{
    InputStates InputState { get; set; }
    
    void StartInteraction(InteractableDetector id);
    void EndInteraction(InteractableDetector id);
}
