using System;
using UnityEngine;
using NaughtyAttributes;


public class Raycast : MonoBehaviour
{
    [SerializeField] private string _raycastName;
    
    [Header("Raycast Infos")]
    [Dropdown("GetVectorValues"), SerializeField]
    private Vector3 _raycastDirection;
    
    private DropdownList<Vector3> GetVectorValues()
    {
        return new DropdownList<Vector3>()
        {
            { "Right",   Vector3.right },
            { "Left",    Vector3.left },
            { "Up",      Vector3.up },
            { "Down",    Vector3.down },
            { "Forward", Vector3.forward },
            { "Back",    Vector3.back }
        };
    }
    [SerializeField, MinValue(0.1f)] private float _raycastDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _showGizmos;

    private bool _isTrigger;
    public bool IsTrigger { get => _isTrigger; }

    public bool ShootRaycast()
    {
        return Physics.Raycast(transform.position, _raycastDirection, _raycastDistance, _layerMask);
    }
    
    private void OnDrawGizmos()
    {
        if (_showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _raycastDirection * _raycastDistance);
        }
    }
}
