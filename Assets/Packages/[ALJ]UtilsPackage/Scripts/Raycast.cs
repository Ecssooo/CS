using System;
using UnityEngine;
using NaughtyAttributes;


public class Raycast : MonoBehaviour
{
    [SerializeField] private string _raycastName;
    
    private enum RaycastType
    {
        Circle,
        Line,
        Box
    }
    
    [Header("Raycast Infos")]
    [SerializeField] private RaycastType _raycastType;
    
    [Dropdown("GetVectorValues"), SerializeField, ShowIf("ShowLineInInspector")]
    private Vector3 _raycastDirection;
    
    private DropdownList<Vector3> GetVectorValues()
    {
        return new DropdownList<Vector3>()
        {
            { "Right",   Vector3.right },
            { "Left",    Vector3.left },
            { "Up",      Vector3.up },
            { "Down",    Vector3.down },
        };
    }
    
    [SerializeField, MinValue(0.1f), ShowIf("ShowLineInInspector")] private float _raycastDistance;
    [SerializeField, MinValue(0.1f), ShowIf("ShowCircleInInspector")] private float _raycastRadius;
    
    [SerializeField, ShowIf("ShowBoxInInspector")] private Vector2 _boxSize;
    
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _layerMask;
    
    [Header("Debug")]
    [SerializeField] private bool _showGizmos;
    [SerializeField] private bool _showDebugInConsole;
    [SerializeField] private bool _runInThisScripts;

    private bool _isTrigger;
    public bool IsTrigger { get => _isTrigger; }

    public bool ShootRaycast()
    {
        switch (_raycastType)
        {
            case RaycastType.Line:
                _isTrigger = Physics2D.Raycast(transform.position + _offset, _raycastDirection, _raycastDistance, _layerMask);
                break;
            case RaycastType.Circle:
                _isTrigger = Physics2D.OverlapCircle(transform.position + _offset, _raycastRadius, _layerMask);
                break;
            case RaycastType.Box:
                _isTrigger = Physics2D.OverlapBox((Vector2)transform.position + (Vector2)_offset, _boxSize,  0, _layerMask);
                break;
        }
        if(_showDebugInConsole) Debug.Log(_isTrigger);
        return _isTrigger;
    }
    
    private void OnDrawGizmos()
    {
        if (_showGizmos)
        {
            Gizmos.color = Color.red;
            switch (_raycastType)
            {
                case RaycastType.Line:
                    Gizmos.DrawRay(transform.position + _offset, _raycastDirection * _raycastDistance);
                    break;
                case RaycastType.Circle:
                    Gizmos.DrawWireSphere(transform.position + _offset, _raycastRadius);
                    break;
                case RaycastType.Box:
                    Gizmos.DrawWireCube(transform.position + _offset, _boxSize);
                    break;
            }
        }
    }

    private void Update()
    {
        if (_runInThisScripts) ShootRaycast();
    }

    private bool ShowLineInInspector() => _raycastType == RaycastType.Line;
    private bool ShowCircleInInspector() => _raycastType == RaycastType.Circle;
    private bool ShowBoxInInspector() => _raycastType == RaycastType.Box;
}
