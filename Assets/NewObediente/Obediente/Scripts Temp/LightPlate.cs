using UnityEngine;

public class LightPlate : MonoBehaviour
{
    public bool isPressed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rLightBox") && gameObject.CompareTag("RedPlate"))
        {
            pushedRed();
            isPressed = true;
        }
        else if (other.gameObject.CompareTag("gLightBox") && gameObject.CompareTag("GreenPlate"))
        {
            pushedGreen();
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("rLightBox") && gameObject.CompareTag("RedPlate"))
        {
            removedRed();
            isPressed = false;
        }
        else if (other.gameObject.CompareTag("gLightBox") && gameObject.CompareTag("GreenPlate"))
        {
            removedGreen();
            isPressed = false;
        }
    }

    void pushedRed()
    {
        Debug.Log("PUSHED LIGHT RED");
    }

    void pushedGreen()
    {
        Debug.Log("PUSHED LIGHT Green");
    }

    void removedRed()
    {
        Debug.Log("LIGHT RED IS GONE");
    }

    void removedGreen()
    {
        Debug.Log("LIGHT GREEN IS GONE");
    }
}
