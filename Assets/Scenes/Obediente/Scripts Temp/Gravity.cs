using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFlipped = false;
    private bool flipPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (flipPlayer || Input.GetKeyDown(KeyCode.Space))
        {
            isFlipped = !isFlipped;
            flipPlayer = false;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            rb.gravityScale = isFlipped ? -1f : 1f;
        }
    }

    public void Flipped(){
        flipPlayer = true;
    }
}

