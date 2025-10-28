// using TMPro;
// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// public class PCReuel : MonoBehaviour
// {
//     //Player
//     public int _playerLives = 3;
//     public float moveSpeed = 8f;
//     private bool _playerImmunity;
//     public float groundCheckDistance = 0.6f;
//     public LayerMask groundLayer;
//     public bool connectedToBox; 
//     public string lastHitHazard;

//     //Mobile Movement 
//     private bool Left;
//     private bool Right;


//     //Lives
//     public GameObject heart1;
//     public GameObject heart2;
//     public GameObject heart3;


//     private Rigidbody2D rb;
//     private bool isGrounded;
//     public Animator animator;
//     private GameObject box;
    
//     //RayCast
//     public float distance = 1f;
//     public LayerMask boxMask;
//     GameObject box1;


//     //Scoring
//     public float totalTime;
//     public int lives;
//     public int fragmentsCollected;

//     //Audio
//     public AudioSource src;
//     public AudioClip playerWalk;
//     public AudioClip boxMove;
   


//     void Start()
//     {   
//         rb = GetComponent<Rigidbody2D>();


//     }

//     void OnDrawGizmos(){

//         Gizmos.color = Color.yellow;
//         Gizmos.DrawLine(transform.position, transform.position + transform.right*distance);
//     }

//     void Update()
//     {   

//         //Updating vars for socring
//         lives = _playerLives;
//         totalTime += Time.deltaTime;

//         //Detecting Boxes
//         Physics2D.queriesStartInColliders = false;
//         RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, distance, boxMask);
      
//         if (hit1.collider != null &&
//             (
//             hit1.collider.gameObject.tag == "rHeavyBox" ||
//             hit1.collider.gameObject.tag == "rLightBox" ||
//             hit1.collider.gameObject.tag == "gHeavyBox" ||
//             hit1.collider.gameObject.tag == "gLightBox"
//             )
//             && Input.GetKeyDown(KeyCode.E))
//         {

//             box1 = hit1.collider.gameObject;
//             box1.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
//             box1.GetComponent<FixedJoint2D>().enabled = true;
//             box1.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
//             connectedToBox = true;
            

//         }else if ( box1 != null && Input.GetKeyUp(KeyCode.E))
//             {
//                 Debug.Log("Released");
//                 box1.GetComponent<FixedJoint2D>().enabled = false;
//                 box1.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
//                 connectedToBox = false;
//                 src.clip = boxMove;
//                 src.Stop();
//             }
        

//         // Player movement
//         float inputX = Input.GetAxisRaw("Horizontal");
//         animator.SetFloat("Speed", Mathf.Abs(inputX));

//         if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)){
//             src.clip = playerWalk;
//             src.Play();

            
//         }else if(Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D)){
//             src.clip = playerWalk;
//             src.Stop ();
//         }

//         //Box Sounds
//         if(
//             box1 != null && 
//             (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) 
//             && connectedToBox){
//                 src.clip = boxMove;
//                 src.Play();
//             }





//         if (inputX != 0)
//         {
//             bool flippers = inputX < 0;
//             transform.rotation = Quaternion.Euler(new Vector3(0f, flippers ? 180f : 0f, 0f));

//         }

//         if (inputX == 0)
//         {
//             // src.Stop();
            

//         }

//         rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);

//         // Ground check
//         Vector2 checkDirection = Physics2D.gravity.normalized;
//         Vector2 origin = (Vector2)transform.position;
//         RaycastHit2D hit = Physics2D.Raycast(origin, checkDirection, groundCheckDistance, groundLayer);
//         isGrounded = hit.collider != null;
//     }

//      //Player Movement in Mobile
//     public void LeftMovement(){
//         Left = true;
//         Debug.Log(Left);
//     }
 



//     void OnTriggerEnter2D(Collider2D other){
        
//         if(!_playerImmunity){
//             if(other.CompareTag("Spikers")||other.CompareTag("LiveWires") || other.CompareTag("Laser"))
//             {   
//                 _playerImmunity = true;
//                 StartCoroutine(PlayerImmuneAni());
//                 StartCoroutine(PlayerImmune());
                
//                 _playerLives -= 1;
//                 LivesCounter();
//                 //Updating Lives 
                
//             }
//         }else if(_playerImmunity){
//             Debug.Log("Immune");   
//         }

//         if(other.CompareTag("Collectible")){
//             fragmentsCollected += 1;
//         }

//         if(other.CompareTag("Spikers")||other.CompareTag("LiveWires") || other.CompareTag("Laser")){
//             lastHitHazard = other.gameObject.tag;
//         }
//     }

//     void LivesCounter(){
    
//         switch(_playerLives){

//             case 0: heart1.SetActive(false);
//                 break;
//             case 1: heart2.SetActive(false);
//                 break;
//             case 2: heart3.SetActive(false);
//                 break;
//             default: Debug.Log("No Damage");
//                 break;
//         }

//     }

//     IEnumerator PlayerImmune(){
//         Debug.Log("Immunity On");
//         yield return new WaitForSeconds (3.0f);
//         Debug.Log("Immunity Off");
//         _playerImmunity = false;
//     }

//     IEnumerator PlayerImmuneAni(){
//         Renderer body = this.gameObject.GetComponent<Renderer>();
            
//             while(_playerImmunity == true){
//             body.enabled = false;
//             yield return new WaitForSeconds(0.1f);
//             body.enabled = true;
//             yield return new WaitForSeconds(0.1f);
            
//             }
//     }
    
   
// }
