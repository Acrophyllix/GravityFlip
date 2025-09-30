using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Plates")]
    public GameObject[] plates;

    [Header("Door Transforms")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Door Settings")]
    public float slideDistance = 2f;
    public float slideSpeed = 5f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    void Start()
    {
        // Save closed positions
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;

        // Calculate open positions
        leftOpenPos = leftClosedPos + Vector3.left * slideDistance;
        rightOpenPos = rightClosedPos + Vector3.right * slideDistance;
    }

    void Update()
    {
        if (IsOpen())
        {
            // Smoothly slide doors open
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftOpenPos, Time.deltaTime * slideSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightOpenPos, Time.deltaTime * slideSpeed);
        }
        else
        {
            // Smoothly slide doors closed
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftClosedPos, Time.deltaTime * slideSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightClosedPos, Time.deltaTime * slideSpeed);
        }
    }

    // Check if all plates are pressed
    public bool IsOpen()
    {
        foreach (GameObject plateObj in plates)
        {
            LightPlate light = plateObj.GetComponent<LightPlate>();
            HeavyPlate heavy = plateObj.GetComponent<HeavyPlate>();
            bool pressed = false;

            if (light != null)
                pressed = light.isPressed;
            else if (heavy != null)
                pressed = heavy.isPressed;

            if (!pressed)
                return false;
        }

        return true;
    }
}
