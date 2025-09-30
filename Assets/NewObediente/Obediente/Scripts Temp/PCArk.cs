using UnityEngine;

public class PCArk : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    public Animator animator;

    private bool boxInRange = false;
    private Rigidbody2D boxRb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (boxInRange && Input.GetKey(KeyCode.E))
        {
            boxRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX; // unlock X for pushing
        }
        else if (boxInRange)
        {
            boxRb.constraints |= RigidbodyConstraints2D.FreezePositionX; // stay locked
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(inputX));

        if (inputX != 0)
        {
            bool flippers = inputX < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }

        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") || other.gameObject.CompareTag("gHeavyBox") ||
            other.gameObject.CompareTag("gLightBox") || other.gameObject.CompareTag("rLightBox"))
        {
            boxRb = other.gameObject.GetComponent<Rigidbody2D>();
            boxRb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            boxInRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") || other.gameObject.CompareTag("gHeavyBox") ||
            other.gameObject.CompareTag("gLightBox") || other.gameObject.CompareTag("rLightBox"))
        {
            boxInRange = false;
            boxRb = null;
        }
    }
}
