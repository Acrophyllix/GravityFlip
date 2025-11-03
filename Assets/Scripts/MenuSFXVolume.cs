using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuSFXVolume : MonoBehaviour
{
    [Header("UI")]
    public Slider sfxSlider;
    public TextMeshProUGUI sfxLabel;

    [Header("Audio")]
    public AudioSource[] sfxAudios; // Assign all SFX AudioSources here

    [Header("Sound Mixer")]
    [SerializeField] private AudioMixer SoundMixer;

    private float sfxValue = 100f;

    private void Start()
    {
        // Initialize PlayerPrefs
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", sfxValue);
            PlayerPrefs.Save();
        }

        // Load saved value
        sfxValue = PlayerPrefs.GetFloat("SFXVolume");
        sfxSlider.value = sfxValue;
        ApplySFXVolume(sfxValue);

        // Apply to AudioMixer
        SoundMixer.SetFloat("SoundEffects", Mathf.Log10(Mathf.Max(sfxValue / 100f, 0.0001f)) * 20f);

        // Add listener
        sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
    }

    private void UpdateSFXVolume(float value)
    {
        ApplySFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
        SoundMixer.SetFloat("SoundEffects", Mathf.Log10(Mathf.Max(value / 100f, 0.0001f)) * 20f);
    }

    private void ApplySFXVolume(float value)
    {
        if (sfxLabel != null)
            sfxLabel.text = "SFX: " + Mathf.RoundToInt(value);

        if (sfxAudios != null)
        {
            foreach (AudioSource sfx in sfxAudios)
            {
                if (sfx != null)
                    sfx.volume = value / 100f;
            }
        }
    }
}
