using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private MusicType musicType = MusicType.CustomMusic;
    [SerializeField] private AudioClip customMusicClip; // Cho phép chọn bất kỳ bài nhạc nào
    [SerializeField] private bool fadeTransition = true;
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private bool stopCurrentMusicFirst = true; // Dừng nhạc hiện tại trước khi phát nhạc mới
    
    private bool hasTriggered = false;
    
    private void Start()
    {
        Debug.Log($"MusicTrigger initialized on {gameObject.name}. Music Type: {musicType}, Custom Clip: {(customMusicClip != null ? customMusicClip.name : "NULL")}");
    }

    public enum MusicType
    {
        BackgroundMusic,  // Nhạc nền mặc định
        BossMusic,        // Nhạc boss
        CustomMusic,      // Nhạc tùy chỉnh (chọn AudioClip riêng)
        StopMusic         // Dừng nhạc
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"MusicTrigger: Something entered trigger! Object: {collision.gameObject.name}, Tag: {collision.tag}");
        
        // Check if player entered
        if (collision.CompareTag("Player"))
        {
            Debug.Log("MusicTrigger: Player detected!");
            
            // If trigger once and already triggered, return
            if (triggerOnce && hasTriggered)
            {
                Debug.Log("MusicTrigger: Already triggered, ignoring...");
                return;
            }

            hasTriggered = true;
            ChangeMusicTrack();
        }
        else
        {
            Debug.LogWarning($"MusicTrigger: Not a player! Tag is '{collision.tag}', expected 'Player'");
        }
    }

    private void ChangeMusicTrack()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager not found!");
            return;
        }

        Debug.Log($"=== Music Trigger activated: {musicType} ===");

        // Stop current music first if enabled
        if (stopCurrentMusicFirst && musicType != MusicType.StopMusic)
        {
            Debug.Log("Stopping current music...");
            AudioManager.Instance.StopMusic(fadeTransition);
        }

        switch (musicType)
        {
            case MusicType.BackgroundMusic:
                Debug.Log("Playing Background Music");
                AudioManager.Instance.PlayBackgroundMusic();
                break;

            case MusicType.BossMusic:
                Debug.Log("Playing Boss Music");
                AudioManager.Instance.PlayBossMusic();
                break;

            case MusicType.CustomMusic:
                if (customMusicClip != null)
                {
                    Debug.Log($"Playing custom music: {customMusicClip.name}");
                    AudioManager.Instance.PlayMusic(customMusicClip, fadeTransition);
                }
                else
                {
                    Debug.LogError("Custom music clip is NOT ASSIGNED! Please assign it in Inspector.");
                }
                break;

            case MusicType.StopMusic:
                Debug.Log("Stopping music");
                AudioManager.Instance.StopMusic(fadeTransition);
                break;
        }
        
        Debug.Log("=== Music Trigger completed ===");
    }

    // Optional: Visualize trigger zone in editor
    private void OnDrawGizmos()
    {
        // Different colors for different music types
        switch (musicType)
        {
            case MusicType.BossMusic:
                Gizmos.color = Color.red;
                break;
            case MusicType.BackgroundMusic:
                Gizmos.color = Color.green;
                break;
            case MusicType.CustomMusic:
                Gizmos.color = Color.cyan;
                break;
            case MusicType.StopMusic:
                Gizmos.color = Color.gray;
                break;
        }
        
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, Vector2.one);
        }
    }
}
