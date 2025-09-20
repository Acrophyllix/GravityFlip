using UnityEngine;

public class HeavyPlate : MonoBehaviour
{
    private Renderer heavyBoxColor;
    private GameObject heavyBox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            
    }


    void OnTriggerEnter2d (Collider other){



        if(other.CompareTag("HeavyBox"))
        {
            pushedPlate();
        }else{

        }


    }
    void pushedPlate ()
    {
        Debug.Log("PUSHED HEAVY");

    }

    void invalidPush ()
    {
        Debug.Log("PUSHED Too Light");

    }
}
