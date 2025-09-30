using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform player;

    [Header("Clamp Range")]
    public float minX = 0.5f;
    public float maxX = 54.5f;

    void Update()
    {
        if (player == null) return;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = newPos;
    }
}
