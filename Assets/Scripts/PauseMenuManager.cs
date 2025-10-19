using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject circularMenu;   // assign CircularMenu here
    public GameObject pauseButton;    // assign PauseButton here

    private bool isPaused = false;
    private bool menuOpen = false;

    void Start()
    {
        circularMenu.SetActive(false);
    }

    // ===== BUTTON FUNCTIONS =====
    public void OnPauseButtonPressed()
    {
        if (!isPaused)
        {
            PauseGame();
            ToggleCircularMenu();
        }
        else if(isPaused)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        circularMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        menuOpen = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SplashScreenScene");
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ToggleCircularMenu()
    {
        menuOpen = !menuOpen;
        circularMenu.SetActive(menuOpen);
    }
}
