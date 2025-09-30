using UnityEngine;

public class HeavyPlate : MonoBehaviour
{
    public bool isPressed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") && gameObject.CompareTag("RedPlate"))
        {
            pushedRed();
            isPressed = true;
        }
        else if (other.gameObject.CompareTag("gHeavyBox") && gameObject.CompareTag("GreenPlate"))
        {
            pushedGreen();
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rHeavyBox") && gameObject.CompareTag("RedPlate"))
        {
            removedRed();
            isPressed = false;
        }
        else if (other.gameObject.CompareTag("gHeavyBox") && gameObject.CompareTag("GreenPlate"))
        {
            removedGreen();
            isPressed = false;
        }
    }

    void pushedRed()
    {
        Debug.Log("PUSHED HEAVY RED");
    }

    void pushedGreen()
    {
        Debug.Log("PUSHED HEAVY Green");
    }

    void removedRed()
    {
        Debug.Log("HEAVY RED IS GONE");
    }

    void removedGreen()
    {
        Debug.Log("HEAVY GREEN IS GONE");
    }
}
