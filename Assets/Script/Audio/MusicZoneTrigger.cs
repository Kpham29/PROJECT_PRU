using UnityEngine;

/// <summary>
/// Trigger to change music when player enters a zone
/// Attach this to a GameObject with a Collider2D (set as Trigger)
/// </summary>
public class MusicZoneTrigger : MonoBehaviour
{
    [Header("Music Zone Settings")]
    [SerializeField] private AudioClip zoneMusic; // Nhạc cho zone này
    [SerializeField] private bool fadeTransition = true;
    [SerializeField] private bool playOnce = true; // Chỉ phát 1 lần khi vào zone
    
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered
        if (!other.CompareTag("Player")) return;
        
        // If playOnce and already triggered, skip
        if (playOnce && hasTriggered) return;
        
        // Play music
        if (zoneMusic != null && AudioManager.Instance != null)
        {
            Debug.Log($"MusicZoneTrigger: Player entered zone, changing music to '{zoneMusic.name}'");
            
            // Stop current music
            AudioManager.Instance.StopMusic(fadeTransition);
            
            // Wait a bit if fading, then play new music
            if (fadeTransition)
            {
                Invoke(nameof(PlayZoneMusic), 0.5f);
            }
            else
            {
                PlayZoneMusic();
            }
            
            hasTriggered = true;
        }
        else
        {
            Debug.LogWarning("MusicZoneTrigger: zoneMusic or AudioManager is NULL!");
        }
    }

    private void PlayZoneMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(zoneMusic, fadeTransition);
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize trigger zone in editor
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.DrawCube(transform.position + (Vector3)box.offset, box.size);
        }
    }
}
