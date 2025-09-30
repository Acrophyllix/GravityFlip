using UnityEngine;

public class Plate : MonoBehaviour
{
    public bool isPressed = false;

    public void Press()
    {
        isPressed = true;
    }

    public void Release()
    {
        isPressed = false;
    }
}
