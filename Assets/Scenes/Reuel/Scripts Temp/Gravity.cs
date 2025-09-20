using UnityEngine;

public class Gravity : MonoBehaviour
{

    //Player Gravity

    private Rigidbody2D playerRB;
    private bool isPFlipped = false;

 
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerFlip();
        }
    }


    
    //Player Gravity
    void playerFlip()
    {

        isPFlipped = !isPFlipped;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        if(isPFlipped)
        {
            playerRB.gravityScale = -1f;
            
        }else if(!isPFlipped)/*Not flipped/False*/
        {
           playerRB.gravityScale = 1f;
        }

    }


  
    

   
}

