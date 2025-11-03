using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCReuel : MonoBehaviour
{
    // Player
    public int _playerLives = 3;
    public float moveSpeed = 8f;
    private bool _playerImmunity;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;
    public bool connectedToBox; 
    public string lastHitHazard;
    private bool isPlayerMovingBox = false;

    // Mobile Movement 
    private bool holdingBox;
    private bool notHoldingBox;
    float inputX = 0f;

    // Lives
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator animator;
    private GameObject box;
    
    // RayCast
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject box1;

    // Scoring
    public float totalTime;
    public int lives;
    public int fragmentsCollected;

    // Audio
    public AudioSource src;
    public AudioSource src2;
    public AudioClip playerWalk;
    public AudioClip boxMove;
   
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        inputX = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distance);
    }

    void Update()
    {   
        // Player movement
        float KeyboardInput = Input.GetAxisRaw("Horizontal");
        float inputTotal = KeyboardInput + inputX;
        animator.SetFloat("Speed", Mathf.Abs(inputTotal));
        
        // Updating vars for scoring
        lives = _playerLives;
        totalTime += Time.deltaTime;

        // Detecting Boxes
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, distance, boxMask);
      
        if (hit1.collider != null &&
            (
            hit1.collider.CompareTag("rHeavyBox") ||
            hit1.collider.CompareTag("rLightBox") ||
            hit1.collider.CompareTag("gHeavyBox") ||
            hit1.collider.CompareTag("gLightBox")
            )
            && holdingBox)
        {
            box1 = hit1.collider.gameObject;
            var boxRb = box1.GetComponent<Rigidbody2D>();
            var joint = box1.GetComponent<FixedJoint2D>();

            // Attach player to box
            boxRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            joint.enabled = true;
            joint.connectedBody = rb;
            connectedToBox = true;

            // Box moving sound
            if (Mathf.Abs(inputTotal) > 0.1f && !src2.isPlaying)
            {
                src2.clip = boxMove;
                src2.loop = true;
                src2.Play();
            }
            else if (Mathf.Abs(inputTotal) <= 0.1f && src2.isPlaying)
            {
                src2.Stop();
            }
        }
        else if (box1 != null && !holdingBox)
        {
            // Detach player from box
            box1.GetComponent<FixedJoint2D>().enabled = false;
            box1.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
            connectedToBox = false;

            if (src2.isPlaying) src2.Stop();
        }

        // Flip player direction
        if (inputTotal != 0)
        {
            bool flippers = inputX < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }

        rb.linearVelocity = new Vector2(inputTotal * moveSpeed, rb.linearVelocity.y);

        // Ground check
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    // Player Movement in Mobile
    public void LeftMovementDown()
    {
        inputX = -1f;
        Debug.Log(isPlayerMovingBox);
        if (!isPlayerMovingBox)
        {
            src.clip = playerWalk;
            src.loop = true;
            src.Play();
        }
    }

    public void OnUp()
    {
        inputX = 0;
        src.Stop();
    }

    public void RightMovementDown()
    {
        inputX = 1f;
        if (!isPlayerMovingBox)
        {
            src.clip = playerWalk;
            src.loop = true;
            src.Play();
        }
    }
 
    public void HoldBox()
    {
        holdingBox = true;
        isPlayerMovingBox = true;
    } 

    public void ReleaseBox()
    {
        holdingBox = false;
        isPlayerMovingBox = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_playerImmunity)
        {
            if (other.CompareTag("Spikers") || other.CompareTag("LiveWires") || other.CompareTag("Laser"))
            {   
                _playerImmunity = true;
                StartCoroutine(PlayerImmuneAni());
                StartCoroutine(PlayerImmune());
                
                _playerLives -= 1;
                LivesCounter();
            }
        }
        else if (_playerImmunity)
        {
            Debug.Log("Immune");   
        }

        if (other.CompareTag("Collectible"))
        {
            fragmentsCollected += 1;
        }

        if (other.CompareTag("Spikers") || other.CompareTag("LiveWires") || other.CompareTag("Laser"))
        {
            lastHitHazard = other.gameObject.tag;
        }
    }

    void LivesCounter()
    {
        switch (_playerLives)
        {
            case 0:
                heart1.SetActive(false);
                break;
            case 1:
                heart2.SetActive(false);
                break;
            case 2:
                heart3.SetActive(false);
                break;
            default:
                Debug.Log("No Damage");
                break;
        }
    }

    IEnumerator PlayerImmune()
    {
        Debug.Log("Immunity On");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Immunity Off");
        _playerImmunity = false;
    }

    IEnumerator PlayerImmuneAni()
    {
        Renderer body = this.gameObject.GetComponent<Renderer>();
        while (_playerImmunity == true)
        {
            body.enabled = false;
            yield return new WaitForSeconds(0.1f);
            body.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
