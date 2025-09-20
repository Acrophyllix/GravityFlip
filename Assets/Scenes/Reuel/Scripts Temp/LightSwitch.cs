using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    public GameObject LightBox;
    private Rigidbody2D[] lbArray;
    private bool isSwitchFlipped = false;
    private int lightBCount;
    //private Tag switchtag;

     //Interface
    public Collider2D player;
    private bool playerInRange = false;

    void Start()
    {
        lightBCount = LightBox.transform.childCount;
        lbToArray();
        
    }

    
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
           lightBox();
        }        
    }

    void lbToArray(){
        
        lbArray = new Rigidbody2D[lightBCount];
        for(int i = 0; i < lightBCount; i++) 
        {
            lbArray[i] = LightBox.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
        }
    }

     //NOT FINAL FOR Box
    void OnTriggerEnter2D (Collider2D other)
    {

            playerInRange = true;
    }

    void OnTriggerExit2D (Collider2D other)
    {

        playerInRange = false;
    }


    void lightBox()
    {
       isSwitchFlipped = !isSwitchFlipped; 
       

       if(isSwitchFlipped)
       {
            for(int i = 0; i < lightBCount; i++) 
            {
                lbArray[i].gravityScale = -1f;
            }
       
       }else if(!isSwitchFlipped)
        {
            for(int i = 0; i < lightBCount; i++) 
            {
                lbArray[i].gravityScale = 1f;
            }
        }
    }

  
    
}
