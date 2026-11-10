using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float maxSpeed = 10f;
    
    private Rigidbody2D rb;
    private float currentSpeed;
    // private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
    }

    void FixedUpdate()
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);
        
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }
}
