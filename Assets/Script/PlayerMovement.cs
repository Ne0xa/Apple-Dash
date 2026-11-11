using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference ugoku;
    
    [Header("Speed Settings")]
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float maxSpeed = 10f;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    
    private Rigidbody2D rb;
    private float currentSpeed;
    private bool isGrounded;
    // private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
    }

    void OnEnable() { ugoku.action.Enable(); }
    void OnDisable() { ugoku.action.Disable(); }

    void Update()
    {
        if (ugoku.action.IsPressed() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    
    void FixedUpdate()
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}