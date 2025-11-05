using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("------------ Audio Mixer ------------")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("------------ Audio Source ------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambientSource;

    [Header("------------ Background Music ------------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip bossMusic;

    [Header("------------ Player SFX ------------")]
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip playerAttack;
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private AudioClip playerDeath;
    [SerializeField] private AudioClip playerFootstep;
    [SerializeField] private AudioClip playerLand;

    [Header("------------ Enemy/Boss SFX ------------")]
    [SerializeField] private AudioClip enemyAttack;
    [SerializeField] private AudioClip enemyHurt;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip bossAttack;
    [SerializeField] private AudioClip bossHurt;
    [SerializeField] private AudioClip bossDeath;
    [SerializeField] private AudioClip bossRoar;

    [Header("------------ Environment SFX ------------")]
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private AudioClip wallTouch;
    [SerializeField] private AudioClip portalIn;
    [SerializeField] private AudioClip portalOut;
    [SerializeField] private AudioClip itemPickup;
    [SerializeField] private AudioClip trapActivate;

    [Header("------------ UI SFX ------------")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip menuOpen;
    [SerializeField] private AudioClip menuClose;

    [Header("------------ Volume Settings ------------")]
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.7f;
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float ambientVolume = 0.5f;

    [Header("------------ Audio Settings ------------")]
    [SerializeField] private float musicFadeDuration = 1.5f;
    [SerializeField] private bool playMusicOnStart = true;

    private Coroutine musicFadeCoroutine;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playMusicOnStart && backgroundMusic != null)
        {
            PlayMusic(backgroundMusic, true);
        }
    }

    private void InitializeAudioSources()
    {
        // Ensure audio sources exist
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        if (ambientSource == null)
        {
            ambientSource = gameObject.AddComponent<AudioSource>();
            ambientSource.loop = true;
            ambientSource.playOnAwake = false;
        }

        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (ambientSource != null) ambientSource.volume = ambientVolume;
    }

    // ==================== MUSIC CONTROLS ====================
    
    public void PlayMusic(AudioClip clip, bool fadeIn = false)
    {
        if (clip == null) return;

        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);

        if (fadeIn)
        {
            musicFadeCoroutine = StartCoroutine(FadeInMusic(clip));
        }
        else
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void StopMusic(bool fadeOut = false)
    {
        if (musicFadeCoroutine != null)
            StopCoroutine(musicFadeCoroutine);

        if (fadeOut)
        {
            musicFadeCoroutine = StartCoroutine(FadeOutMusic());
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void PlayBossMusic()
    {
        if (bossMusic != null)
        {
            PlayMusic(bossMusic, true);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic, true);
        }
    }

    private IEnumerator FadeInMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.volume = 0f;
        musicSource.Play();

        float elapsed = 0f;
        while (elapsed < musicFadeDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, musicVolume, elapsed / musicFadeDuration);
            yield return null;
        }

        musicSource.volume = musicVolume;
        musicFadeCoroutine = null;
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < musicFadeDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / musicFadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = musicVolume;
        musicFadeCoroutine = null;
    }

    // ==================== SFX CONTROLS ====================

    public void PlaySFX(AudioClip clip, float volumeScale = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    // Player SFX
    public void PlayPlayerJump() => PlaySFX(playerJump);
    public void PlayPlayerAttack() => PlaySFX(playerAttack);
    public void PlayPlayerHurt() => PlaySFX(playerHurt);
    public void PlayPlayerDeath() => PlaySFX(playerDeath);
    public void PlayPlayerFootstep() => PlaySFX(playerFootstep, 0.5f);
    public void PlayPlayerLand() => PlaySFX(playerLand);

    // Enemy/Boss SFX
    public void PlayEnemyAttack() => PlaySFX(enemyAttack);
    public void PlayEnemyHurt() => PlaySFX(enemyHurt);
    public void PlayEnemyDeath() => PlaySFX(enemyDeath);
    public void PlayBossAttack() => PlaySFX(bossAttack);
    public void PlayBossHurt() => PlaySFX(bossHurt);
    public void PlayBossDeath() => PlaySFX(bossDeath);
    public void PlayBossRoar() => PlaySFX(bossRoar, 1.2f);

    // Environment SFX
    public void PlayCheckpoint() => PlaySFX(checkpoint);
    public void PlayWallTouch() => PlaySFX(wallTouch, 0.3f);
    public void PlayPortalIn() => PlaySFX(portalIn);
    public void PlayPortalOut() => PlaySFX(portalOut);
    public void PlayItemPickup() => PlaySFX(itemPickup);
    public void PlayTrapActivate() => PlaySFX(trapActivate);

    // UI SFX
    public void PlayButtonClick() => PlaySFX(buttonClick);
    public void PlayMenuOpen() => PlaySFX(menuOpen);
    public void PlayMenuClose() => PlaySFX(menuClose);

    // ==================== VOLUME CONTROLS ====================

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        
        // Use Audio Mixer if available, otherwise fallback to direct volume control
        if (audioMixer != null)
        {
            // Convert 0-1 to decibels (-80 to 0)
            float db = volume > 0 ? 20f * Mathf.Log10(volume) : -80f;
            audioMixer.SetFloat("MusicVolume", db);
        }
        else if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        
        if (audioMixer != null)
        {
            float db = volume > 0 ? 20f * Mathf.Log10(volume) : -80f;
            audioMixer.SetFloat("SFXVolume", db);
        }
        else if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
    }

    public void SetAmbientVolume(float volume)
    {
        ambientVolume = Mathf.Clamp01(volume);
        
        if (audioMixer != null)
        {
            float db = volume > 0 ? 20f * Mathf.Log10(volume) : -80f;
            audioMixer.SetFloat("AmbientVolume", db);
        }
        else if (ambientSource != null)
        {
            ambientSource.volume = ambientVolume;
        }
    }

    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
    public float GetAmbientVolume() => ambientVolume;

    // ==================== AMBIENT SOUNDS ====================

    public void PlayAmbient(AudioClip clip)
    {
        if (clip != null && ambientSource != null)
        {
            ambientSource.clip = clip;
            ambientSource.Play();
        }
    }

    public void StopAmbient()
    {
        if (ambientSource != null)
            ambientSource.Stop();
    }

    // ==================== PAUSE/UNPAUSE MUSIC ====================
    
    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }
    
    public void UnpauseMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }
}
