using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static int collectedTotal = 0;

    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collectedTotal++;
            Debug.Log("Collected: " + collectedTotal);
            Destroy(gameObject);
        }
    }
}
