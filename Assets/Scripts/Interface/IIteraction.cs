using UnityEngine;

public interface IIteraction
{
    bool InputPerformed { get; set; }
    
    void StartInteraction(InteractableDetector id);
    void EndInteraction(InteractableDetector id);
}
