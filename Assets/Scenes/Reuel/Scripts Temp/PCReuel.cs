using UnityEngine;

public class PCReuel : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float airControlFactor = 0.6f; // less control in air

    [Header("Jumping")]
    public float jumpForce = 12f;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    [Header("Jump Buffer & Coyote Time")]
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float targetSpeed;
    private float currentSpeed;

    [Header("Animator")]
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ---------------- MOVEMENT ----------------
        float inputX = Input.GetAxisRaw("Horizontal");
        targetSpeed = inputX * moveSpeed;

        //Animation
        //Run
        animator.SetFloat("Speed", Mathf.Abs(inputX));

        //To properflip horizontally
        if (targetSpeed != 0)//means running
        {
            bool flippers = targetSpeed < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }
        
        // If airborne, reduce control
        float accel = isGrounded ? acceleration : acceleration * airControlFactor;
        float decel = isGrounded ? deceleration : deceleration * airControlFactor;

        // Smooth speed
        if (Mathf.Abs(inputX) > 0.01f)
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accel * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decel * Time.deltaTime);

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        // ---------------- GROUND CHECK ----------------
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Coyote Time
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump Buffer
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // ---------------- JUMPING ----------------
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            // Jump opposite gravity
            Vector2 jumpDir = -Physics2D.gravity.normalized;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reset vertical velocity
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);

            jumpBufferCounter = 0;
        }

            // Variable jump height (short hop if released early)
            if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && !isGrounded)
        {
            // Check if moving against gravity (i.e., still rising)
            if (Vector2.Dot(rb.linearVelocity, -Physics2D.gravity.normalized) > 0)
            {
            rb.linearVelocity *= 0.5f; // cut velocity in half
            }
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        Gizmos.DrawLine(origin, origin + checkDirection * groundCheckDistance);
    }
}