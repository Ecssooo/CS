using System;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour, IIteraction
{

    [SerializeField] private bool _inputPerformed;
    [SerializeField] private float _timeToOpen;
    [SerializeField] private int _healthValue;
    [SerializeField] private UnityEvent _onChestOpen;
    
    //Private flield
    private float t_open;
    private enum ChestStates{ Open, Close}
    private ChestStates s_chestStates = ChestStates.Close;
    
    public bool InputPerformed { get; set; }

    private void Start()
    {
        InputPerformed = _inputPerformed;
    }

    public void StartInteraction(InteractableDetector id)
    {
        if (t_open >= _timeToOpen && s_chestStates == ChestStates.Close)
        {
            s_chestStates = ChestStates.Open;
            id.GetComponentInParent<Health>().Regeneration(_healthValue);
            _onChestOpen?.Invoke();
        }else
        {
            t_open += Time.deltaTime;
        }
    }

    public void EndInteraction(InteractableDetector id)
    {
        Debug.Log("End interaction with chest");
    }
}
