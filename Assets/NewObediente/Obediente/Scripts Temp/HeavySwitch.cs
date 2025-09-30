using UnityEngine;

public class HeavySwitch : MonoBehaviour
{
    public GameObject HeavyBox;
    private Rigidbody2D[] hbArray;
    private int heavyBCount;

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
        heavyBCount = HeavyBox.transform.childCount;
        hbToArray();
        fConstraint();

        // Apply Inspector checkbox state
        ApplySwitchState(flipped);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            flipped = !flipped;
            ApplySwitchState(flipped);
        }
    }

    void hbToArray()
    {
        hbArray = new Rigidbody2D[heavyBCount];
        for (int i = 0; i < heavyBCount; i++)
        {
            hbArray[i] = HeavyBox.transform.GetChild(i).GetComponent<Rigidbody2D>();
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

        // Toggle gravity of heavy boxes
        float gravity = state ? -1f : 1f;
        for (int i = 0; i < heavyBCount; i++)
        {
            if (hbArray[i].CompareTag("rHeavyBox") || hbArray[i].CompareTag("gHeavyBox"))
            {
                hbArray[i].gravityScale = gravity;
            }
        }
    }

    void fConstraint()
    {
        for (int i = 0; i < heavyBCount; i++)
        {
            hbArray[i].constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
