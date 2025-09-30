using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Plates")]
    public GameObject[] plates; // Assign plate GameObjects in Inspector

    [Header("Gate Settings")]
    public Transform gate;         // The gate object (assign in Inspector)
    public float liftHeight = 3f;  // How high the gate retracts
    public float liftSpeed = 5f;   // Speed of lifting/lowering

    private Vector3 closedPos;
    private Vector3 openPos;

    void Start()
    {
        // Save the closed position
        closedPos = gate.position;

        // Calculate the open (lifted) position
        openPos = closedPos + Vector3.up * liftHeight;
    }

    void Update()
    {
        if (IsOpen())
        {
            // Smoothly move the gate upward
            gate.position = Vector3.Lerp(gate.position, openPos, Time.deltaTime * liftSpeed);
        }
        else
        {
            // Smoothly move the gate back down
            gate.position = Vector3.Lerp(gate.position, closedPos, Time.deltaTime * liftSpeed);
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
