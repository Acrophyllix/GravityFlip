using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuVolume : MonoBehaviour
{
    [Header("UI")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeLabel;

    [Header("Audio")]
    public AudioSource musicAudio;
    private float bgValue = 100f;

    [Header("Sound Mixer")]
    [SerializeField] private AudioMixer SoundMixer;

    private void Start()
    {
        // Set default value if missing
        if (!PlayerPrefs.HasKey("MenuVolume"))
        {
            PlayerPrefs.SetFloat("MenuVolume", bgValue);
            PlayerPrefs.Save();
        }

        // Load saved volume
        bgValue = PlayerPrefs.GetFloat("MenuVolume");

        // Apply to UI and Audio
        volumeSlider.value = bgValue;
        ApplyVolume(bgValue);

        // Listen for slider changes
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        PlayerPrefs.SetFloat("MenuVolume", value);
        PlayerPrefs.Save();
        ApplyVolume(value);

        // Apply to AudioMixer (convert 0–100 linear to decibels)
        SoundMixer.SetFloat("BackgroundMusic", Mathf.Log10(Mathf.Max(value / 100f, 0.0001f)) * 20f);
    }

    private void ApplyVolume(float value)
    {
        if (volumeLabel != null)
            volumeLabel.text = "Volume: " + Mathf.RoundToInt(value);

        if (musicAudio != null)
            musicAudio.volume = value / 100f;

        // Also set mixer volume when applying
        SoundMixer.SetFloat("BackgroundMusic", Mathf.Log10(Mathf.Max(value / 100f, 0.0001f)) * 20f);
    }
}
