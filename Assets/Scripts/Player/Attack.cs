using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    
    [Header("Attack Settings")]
    [SerializeField, MinValue(0)] private int _maxDamagePerFrame ;
    [SerializeField, Label("Attack Input")] private InputActionReference attackAction;
    
    [SerializeField] private string _animationName;
    
    [SerializeField] private bool _attackOnFrame;
    [SerializeField, HideIf("_attackOnFrame")] private float _attackDuration;
    [SerializeField] private UnityEvent OnAttack; 
    
    
    //Private field
    private bool _canAttack;
    private bool _isAttacking;
    //Ennemy infos
    private List<Health> _ennemyHeatlh = new List<Health>();
    private bool _isTrigger;

    
    private void Start()
    {
        attackAction.action.started += DoAttack;
        _canAttack = true;
    }

    private void OnDestroy()
    {
        attackAction.action.started -= DoAttack;
    }

    private void Update()
    {
        CheckIfEnnemyDead();
        if (_isAttacking)
        {
            ApplyDamage();
        }
    }

    void DoAttack(InputAction.CallbackContext context)
    {
        if(_canAttack)
        {
            OnAttack?.Invoke();
            if (_isTrigger)
            {
                if(!_attackOnFrame)
                    StartCoroutine(AttackDuration());
                else
                    ApplyDamage();
            }
        }
    }

    void ApplyDamage()
    {
        if (_ennemyHeatlh == null) return;
        else
        {
            foreach (Health _ennemy in _ennemyHeatlh)
            {
                _ennemy.TakeDamage(_maxDamagePerFrame);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health component))
        {
            _ennemyHeatlh.Add(component);
            _isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health component))
        {
            int index = 0;
            foreach (Health ennemy in _ennemyHeatlh.ToList())
            {
                if (ennemy == component)
                {
                    _ennemyHeatlh.RemoveAt(index);
                    index++;
                }
            }
            if(_ennemyHeatlh.Count == 0) _isTrigger = false;
        }
    }

    IEnumerator AttackDuration()
    {
        _canAttack = false;
        _isAttacking = true;
        yield return new WaitForSeconds(_attackDuration);
        _isAttacking = false;
        _canAttack = true;
    }
    

    void CheckIfEnnemyDead()
    {
        if (_ennemyHeatlh == null) return;
        foreach (Health ennemy in _ennemyHeatlh.ToList()) 
        {
            if (ennemy.IsDead) 
            {
                int index = _ennemyHeatlh.FindIndex(0,health =>  ennemy);
                _ennemyHeatlh.RemoveAt(index);
                _isTrigger = false; 
            }
        }
    }
}
