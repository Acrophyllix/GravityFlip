using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform player; // Assign your player here

    [Header("Clamp Settings")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;

    void LateUpdate()
    {
        if (player == null)
            return;

        // Follow player's X position
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        // Keep Y and Z the same
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
