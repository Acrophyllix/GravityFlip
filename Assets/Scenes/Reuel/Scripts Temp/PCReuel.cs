using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PCReuel : MonoBehaviour
{
    //Player
    [SerializeField] private int _playerLives = 3;
    public float moveSpeed = 8f;
    private bool _playerImmunity;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;




    //Lives
    public TextMeshProUGUI livesText;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator animator;
    private GameObject box;
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject box1;

    void Start()
    {   
        livesText.text = _playerLives.ToString();
        rb = GetComponent<Rigidbody2D>();


    }

    void Update()
    {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, distance, boxMask);
      
        if (hit1.collider != null &&
            (
            hit1.collider.gameObject.tag == "rHeavyBox" ||
            hit1.collider.gameObject.tag == "rLightBox" ||
            hit1.collider.gameObject.tag == "gHeavyBox" ||
            hit1.collider.gameObject.tag == "gLightBox"
            )
            && Input.GetKeyDown(KeyCode.E))
        {
            box1 = hit1.collider.gameObject;
            box1.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            box1.GetComponent<FixedJoint2D>().enabled = true;
            box1.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            
        }else if (hit1.collider != null &&
            (
            hit1.collider.gameObject.tag == "rHeavyBox" ||
            hit1.collider.gameObject.tag == "rLightBox" ||
            hit1.collider.gameObject.tag == "gHeavyBox" ||
            hit1.collider.gameObject.tag == "gLightBox"
            )
            &Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Released");
                box1.GetComponent<FixedJoint2D>().enabled = false;
                box1.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
                
                
            }
        

        // Player movement
        float inputX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(inputX));

        if (inputX != 0)
        {
            bool flippers = inputX < 0;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));
        }

        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

        // Ground check
        Vector2 checkDirection = Physics2D.gravity.normalized;
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(!_playerImmunity){
            if(other.CompareTag("Spikers")||other.CompareTag("LiveWires"))
            {   
                _playerImmunity = true;
                StartCoroutine(PlayerImmuneAni());
                StartCoroutine(PlayerImmune());
                
                _playerLives -= 1;
                livesText.text = _playerLives.ToString();
            }
        }else if(_playerImmunity){
            Debug.Log("Immune");   
        }
    }

    IEnumerator PlayerImmune(){
        Debug.Log("Immunity On");
        yield return new WaitForSeconds (3.0f);
        Debug.Log("Immunity Off");
        _playerImmunity = false;
    }

    IEnumerator PlayerImmuneAni(){
        Renderer body = this.gameObject.GetComponent<Renderer>();
            
            while(_playerImmunity == true){
            body.enabled = false;
            yield return new WaitForSeconds(0.1f);
            body.enabled = true;
            yield return new WaitForSeconds(0.1f);
            
            }
    }
   
 
}
