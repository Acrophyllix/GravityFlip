using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject LightBox;
    private Rigidbody2D[] lbArray;
    private int lightBCount;
    private bool flippedSwitch;

    // Interface
    public Collider2D player;
    private bool playerInRange = false;

    // Lever Sprite Swap
    public SpriteRenderer leverRenderer; // Assign the lever's SpriteRenderer
    public Sprite leverOffSprite;        // Lever in "off" position
    public Sprite leverOnSprite;         // Lever in "on" position

    [Header("Level Settings")]
    public bool flipped = false; // Checkbox in Inspector

    void Start()
    {
        lightBCount = LightBox.transform.childCount;
        lbToArray();
        fConstraint();

        // Apply Inspector checkbox state
        ApplySwitchState(flipped);
    }

    void Update()
    {
        if (playerInRange && flippedSwitch)
        {
            flipped = !flipped;
            flippedSwitch = false;
            ApplySwitchState(flipped);
        }
    }

    void lbToArray()
    {
        lbArray = new Rigidbody2D[lightBCount];
        for (int i = 0; i < lightBCount; i++)
        {
            lbArray[i] = LightBox.transform.GetChild(i).GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }

    void ApplySwitchState(bool state)
    {
        // Update lever sprite
        leverRenderer.sprite = state ? leverOnSprite : leverOffSprite;

        // Toggle gravity of boxes
        float gravity = state ? -1f : 1f;
        for (int i = 0; i < lightBCount; i++)
        {
            if (lbArray[i].CompareTag("rLightBox") || lbArray[i].CompareTag("gLightBox"))
            {
                lbArray[i].gravityScale = gravity;
            }
        }
    }

    void fConstraint()
    {
        for (int i = 0; i < lightBCount; i++)
        {
            lbArray[i].constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    
    public void FlippedSwitch(){
        if (playerInRange)
        {
            flipped = !flipped;
            ApplySwitchState(flipped);
        }
    }
}
