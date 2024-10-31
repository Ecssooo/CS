using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Header("Event settings")]
    [SerializeField] private bool _eventOnTime;
    [SerializeField, ShowIf("_eventOnTime")] private float _delayBetweenEvent;
    
    [SerializeField] private UnityEvent OnTrigger;
    
    //Private field
    private Coroutine _eventDelay;
    private Dictionary<Collider, Coroutine> _allCoroutines = new Dictionary<Collider, Coroutine>();
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            if (!_eventOnTime)
            {
                OnTrigger?.Invoke();
            }
            else if (_eventOnTime)
            {
                _allCoroutines.Add(other, StartCoroutine(EventDelay()));
                IEnumerator EventDelay()
                {
                    while (true)
                    {
                        OnTrigger?.Invoke();
                        yield return new WaitForSeconds(_delayBetweenEvent);
                    }
                }  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && _eventOnTime)
        {
            StopCoroutine(_allCoroutines[other]);
            _allCoroutines.Remove(other);
        }
    }
}
