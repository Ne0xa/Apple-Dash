using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] InputActionReference ugoku;
    
    [Header("Speed Settings")]
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float maxSpeed = 10f;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private float currentSpeed;
    private bool jumpRequested;
    // private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
    }

    void OnEnable()
    {
        ugoku.action.Enable();
        ugoku.action.performed += OnJump;
    }

    void OnDisable()
    {
        ugoku.action.performed -= OnJump;
        ugoku.action.Disable();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        jumpRequested = true;
    }
    
    void FixedUpdate()
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
        
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float yVelocity = rb.linearVelocity.y;

        if (jumpRequested && isGrounded)
        {
            yVelocity = jumpForce;
        }
        jumpRequested = false;
        
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
