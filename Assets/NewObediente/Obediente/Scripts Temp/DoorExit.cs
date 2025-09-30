using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    private HeavySwitch heavySwitch;
    private LightSwitch[] lightSwitches;
    private bool levelCompleted = false; // Ensure next level triggers only once

    void Start()
    {
        // Safely find the first HeavySwitch in the scene
        heavySwitch = Object.FindFirstObjectByType<HeavySwitch>();

        // Optionally find all LightSwitch objects in the scene
        lightSwitches = Object.FindObjectsByType<LightSwitch>(FindObjectsSortMode.None);

        if (heavySwitch == null)
        {
            Debug.Log("No HeavySwitch found in the scene. Only LightSwitches will be checked.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            // Check if all switches are flipped
            bool allHeavyFlipped = heavySwitch == null || heavySwitch.flipped;
            bool allLightFlipped = true;

            foreach (var ls in lightSwitches)
            {
                if (!ls.flipped)
                {
                    allLightFlipped = false;
                    break;
                }
            }

            if (allHeavyFlipped && allLightFlipped)
            {
                levelCompleted = true;
                GoToNextLevel();
            }
        }
    }

    private void GoToNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // Loop back to first scene if last level
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }

        SceneManager.LoadScene(nextIndex);
    }
}
