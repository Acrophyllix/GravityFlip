using UnityEngine;

public class HeavySwitch : MonoBehaviour
{
    
    public GameObject HeavyBox;
    private Rigidbody2D[] hbArray;
    private bool isSwitchFlipped = false;
    private int heavyBCount;
  

     //Interface
    public Collider2D player;
    private bool playerInRange = false;

    void Start()
    {
        heavyBCount = HeavyBox.transform.childCount;
     
        hbToArray();
        
    }

    
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
           heavyBox();
        }        
    }

    void hbToArray(){
        
        hbArray = new Rigidbody2D[heavyBCount];
        for(int i = 0; i < heavyBCount; i++) 
        {
            hbArray[i] = HeavyBox.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
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


    void heavyBox()
    {
       isSwitchFlipped = !isSwitchFlipped; 
       

       if(isSwitchFlipped)
       {
            for(int i = 0; i < heavyBCount; i++) 
            {
                hbArray[i].gravityScale = -1f;
            }
       
       }else if(!isSwitchFlipped)
        {
            for(int i = 0; i < heavyBCount; i++) 
            {
                hbArray[i].gravityScale = 1f;
            }
        }
    }

  
    
}
