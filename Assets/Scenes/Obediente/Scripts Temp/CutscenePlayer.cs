using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton;
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        // Make sure the button works
        skipButton.onClick.AddListener(GoToMainMenu);

        // When the video finishes, go to main menu automatically
        videoPlayer.loopPointReached += OnVideoEnd;

        // Optional: Hide skip button for the first 2 seconds
        skipButton.gameObject.SetActive(false);
        Invoke(nameof(ShowSkipButton), 2f);
    }

    void ShowSkipButton()
    {
        skipButton.gameObject.SetActive(true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        GoToMainMenu();
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
