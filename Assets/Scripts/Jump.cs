using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class Jump : MonoBehaviour
{
    [Header("Player Infos")]
    [SerializeField] private Player _player;
    [SerializeField, Label("Rigidbody")] private Rigidbody rb;
    private InputAction jumpAction;
    
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce;
    public float JumpForce { get => _jumpForce; }
    private bool canJump;

    [Header("Raycast")] 
    [SerializeField] private Raycast _groundRaycast;
    private bool _isOnGround;
    
    private void Awake()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void FixedUpdate()
    {
        if (_isOnGround)
        {
            canJump = true;
        }
        if (GetJumpInput() && canJump)
        {
            ApplyJump(GetJumpForce());
        }
    }

    private void Update()
    {
        _isOnGround = _groundRaycast.ShootRaycast();
    }


    public float InitJumpForce() => _jumpForce;
    public float GetJumpForce() => _player.CurrentJumpForce;

    public bool GetJumpInput() => jumpAction.phase == InputActionPhase.Performed; 
    
    public void ApplyJump(float force)
    {
        Vector3 jumpVector = new Vector3(0, force, 0);
        rb.MovePosition(transform.position + jumpVector * Time.fixedDeltaTime);
    }
}
