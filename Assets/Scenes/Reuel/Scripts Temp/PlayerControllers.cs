using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheckBottom;  // Place empty GameObject at feet
    public Transform groundCheckTop;     // Place empty GameObject at head
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Timing")]
    public float coyoteTime = 0.1f;   // grace period after leaving ground
    public float jumpBufferTime = 0.1f; // buffer for pressing jump early

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastGroundedTime;
    private float lastJumpPressedTime;

    private float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Horizontal Movement Input ---
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- Track Ground State ---
        CheckGrounded();

        if (isGrounded)
            lastGroundedTime = Time.time;

        // --- Jump Input ---
        if (Input.GetButtonDown("Jump")) // Space, W, or UpArrow by default
            lastJumpPressedTime = Time.time;

        // --- Perform Jump ---
        if (lastGroundedTime + coyoteTime > Time.time &&
            lastJumpPressedTime + jumpBufferTime > Time.time)
        {
            Jump();
            lastJumpPressedTime = -999f; // reset
            lastGroundedTime = -999f;
        }

        // --- Gravity Flip (example: press "F") ---
        if (Input.GetKeyDown(KeyCode.F))
            FlipGravity();
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void CheckGrounded()
    {
        if (Physics2D.gravity.y < 0)
        {
            // gravity down → check bottom point
            isGrounded = Physics2D.OverlapCircle(groundCheckBottom.position, groundCheckRadius, groundLayer);
        }
        else
        {
            // gravity up → check top point
            isGrounded = Physics2D.OverlapCircle(groundCheckTop.position, groundCheckRadius, groundLayer);
        }
    }

    void Jump()
    {
        // Reset vertical velocity before applying jump
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        // Apply force opposite to gravity
        Vector2 jumpDir = -Physics2D.gravity.normalized;
        rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
    }

    void FlipGravity()
    {
        Physics2D.gravity *= -1; // flip direction
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z); // flip sprite vertically
    }

    void OnDrawGizmosSelected()
    {
        // Draw overlap circles in scene view for debugging
        if (groundCheckBottom != null)
            Gizmos.DrawWireSphere(groundCheckBottom.position, groundCheckRadius);

        if (groundCheckTop != null)
            Gizmos.DrawWireSphere(groundCheckTop.position, groundCheckRadius);
    }
}
