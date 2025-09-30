using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFlipped = !isFlipped;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            rb.gravityScale = isFlipped ? -1f : 1f;
        }
    }
}
