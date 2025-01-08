using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone3D : MonoBehaviour
{
    private enum EventType
    {
        OnOneFrame,
        OnTime
    }   
    
    [Header("Event settings")]
    [SerializeField] private EventType _eventOnTime;
    [SerializeField, ShowIf("ShowEventTimerInInspector")] private float _delayBetweenEvent;

    [SerializeField] private Component _component;
    [SerializeField] private UnityEvent OnTrigger;
    
    [Header("Debug")]
    [SerializeField] private bool _showDebugInConsole;
    [SerializeField] private bool _showGizmo;

    [SerializeField, ShowIf("_showGizmo")] private Collider2D collider2D;
    
    //Private field
    private Type _componentType;
    private Coroutine _eventDelay;
    private Dictionary<Collider, Coroutine> _allCoroutines = new();

    private void Start()
    {
        _componentType = _component.GetType();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(_showDebugInConsole) Debug.Log("Trigger");
        if(other.TryGetComponent(_componentType, out Component component))
        {
            switch (_eventOnTime)
            {
                case (EventType.OnOneFrame):
                    OnTrigger?.Invoke();
                    break;
                case (EventType.OnTime):
                    _allCoroutines.Add(other, StartCoroutine(EventDelay()));

                    IEnumerator EventDelay()
                    {
                        while (true)
                        {
                            OnTrigger?.Invoke();
                            yield return new WaitForSeconds(_delayBetweenEvent);
                        }
                    }
                    break;
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(_componentType, out Component component) && _eventOnTime == EventType.OnTime)
        {
            StopCoroutine(_allCoroutines[other]);
            _allCoroutines.Remove(other);
        }
    }

    private bool ShowEventTimerInInspector() => _eventOnTime == EventType.OnTime;
    
    
    void OnDrawGizmos()
    {
        if (_showGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(collider2D.bounds.center, collider2D.bounds.size);
        }
    }
}