using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider ambientVolumeSlider;

    [Header("Volume Text (Optional)")]
    [SerializeField] private Text musicVolumeText;
    [SerializeField] private Text sfxVolumeText;
    [SerializeField] private Text ambientVolumeText;

    [Header("Mute Toggles (Optional)")]
    [SerializeField] private Toggle musicMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    private float previousMusicVolume;
    private float previousSFXVolume;

    private void Start()
    {
        InitializeSliders();
        LoadVolumeSettings();
    }

    private void InitializeSliders()
    {
        // Setup slider listeners
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.minValue = 0f;
            musicVolumeSlider.maxValue = 1f;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.minValue = 0f;
            sfxVolumeSlider.maxValue = 1f;
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        if (ambientVolumeSlider != null)
        {
            ambientVolumeSlider.minValue = 0f;
            ambientVolumeSlider.maxValue = 1f;
            ambientVolumeSlider.onValueChanged.AddListener(OnAmbientVolumeChanged);
        }

        // Setup toggle listeners
        if (musicMuteToggle != null)
            musicMuteToggle.onValueChanged.AddListener(OnMusicMuteToggled);

        if (sfxMuteToggle != null)
            sfxMuteToggle.onValueChanged.AddListener(OnSFXMuteToggled);
    }

    private void LoadVolumeSettings()
    {
        if (AudioManager.Instance == null) return;

        // Load saved volumes from PlayerPrefs
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", AudioManager.Instance.GetMusicVolume());
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", AudioManager.Instance.GetSFXVolume());
        float ambientVolume = PlayerPrefs.GetFloat("AmbientVolume", AudioManager.Instance.GetAmbientVolume());

        // Set AudioManager volumes
        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
        AudioManager.Instance.SetAmbientVolume(ambientVolume);

        // Update UI
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = musicVolume;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = sfxVolume;

        if (ambientVolumeSlider != null)
            ambientVolumeSlider.value = ambientVolume;

        UpdateVolumeTexts();
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        }

        UpdateVolumeText(musicVolumeText, value);

        // Play a test sound when adjusting
        if (AudioManager.Instance != null && value > 0)
            AudioManager.Instance.PlayButtonClick();
    }

    private void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);
            PlayerPrefs.SetFloat("SFXVolume", value);
            PlayerPrefs.Save();
        }

        UpdateVolumeText(sfxVolumeText, value);

        // Play a test sound when adjusting
        if (AudioManager.Instance != null && value > 0)
            AudioManager.Instance.PlayButtonClick();
    }

    private void OnAmbientVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetAmbientVolume(value);
            PlayerPrefs.SetFloat("AmbientVolume", value);
            PlayerPrefs.Save();
        }

        UpdateVolumeText(ambientVolumeText, value);
    }

    private void OnMusicMuteToggled(bool isMuted)
    {
        if (AudioManager.Instance == null) return;

        if (isMuted)
        {
            previousMusicVolume = AudioManager.Instance.GetMusicVolume();
            AudioManager.Instance.SetMusicVolume(0f);
        }
        else
        {
            AudioManager.Instance.SetMusicVolume(previousMusicVolume);
        }
    }

    private void OnSFXMuteToggled(bool isMuted)
    {
        if (AudioManager.Instance == null) return;

        if (isMuted)
        {
            previousSFXVolume = AudioManager.Instance.GetSFXVolume();
            AudioManager.Instance.SetSFXVolume(0f);
        }
        else
        {
            AudioManager.Instance.SetSFXVolume(previousSFXVolume);
            AudioManager.Instance.PlayButtonClick();
        }
    }

    private void UpdateVolumeTexts()
    {
        if (musicVolumeSlider != null)
            UpdateVolumeText(musicVolumeText, musicVolumeSlider.value);

        if (sfxVolumeSlider != null)
            UpdateVolumeText(sfxVolumeText, sfxVolumeSlider.value);

        if (ambientVolumeSlider != null)
            UpdateVolumeText(ambientVolumeText, ambientVolumeSlider.value);
    }

    private void UpdateVolumeText(Text textComponent, float value)
    {
        if (textComponent != null)
        {
            textComponent.text = Mathf.RoundToInt(value * 100f) + "%";
        }
    }

    // Public methods for buttons
    public void ResetToDefault()
    {
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = 0.7f;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = 1f;

        if (ambientVolumeSlider != null)
            ambientVolumeSlider.value = 0.5f;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
    }

    public void TestMusicVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBackgroundMusic();
    }

    public void TestSFXVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPlayerAttack();
    }
}
