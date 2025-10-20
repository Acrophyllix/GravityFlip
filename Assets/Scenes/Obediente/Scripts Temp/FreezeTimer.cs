using UnityEngine;

public class FreezeTimer : MonoBehaviour
{
    [SerializeField] private WinLose winLoseScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winLoseScript.SetTimerEnabled(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winLoseScript.SetTimerEnabled(true);
        }
    }
}
