using UnityEngine;
using System.Collections;

public class SceneMusicManager : MonoBehaviour
{
    [Header("Scene Music Settings")]
    [SerializeField] private AudioClip sceneMusic; // Nhạc cho scene này
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool fadeTransition = true;
    [SerializeField] private bool stopPreviousMusic = true;
    [SerializeField] private float delayBeforePlay = 0.1f; // Delay để đảm bảo AudioManager đã sẵn sàng

    private void Start()
    {
        Debug.Log($"SceneMusicManager.Start() called on GameObject: {gameObject.name}, playOnStart={playOnStart}, sceneMusic={sceneMusic?.name}");
        
        // Check for multiple instances
        var allManagers = FindObjectsOfType<SceneMusicManager>();
        if (allManagers.Length > 1)
        {
            Debug.LogWarning($"SceneMusicManager: Found {allManagers.Length} instances in scene! This may cause conflicts.");
            foreach (var manager in allManagers)
            {
                Debug.Log($"  - {manager.gameObject.name}: sceneMusic={manager.sceneMusic?.name}");
            }
        }
        
        if (playOnStart)
        {
            StartCoroutine(PlaySceneMusicDelayed());
        }
        else
        {
            Debug.LogWarning("SceneMusicManager: playOnStart is FALSE, music will not play!");
        }
    }

    private IEnumerator PlaySceneMusicDelayed()
    {
        // Wait a bit to ensure AudioManager is ready
        yield return new WaitForSeconds(delayBeforePlay);

        if (AudioManager.Instance == null)
        {
            Debug.LogError("SceneMusicManager: AudioManager not found!");
            yield break;
        }

        Debug.Log($"SceneMusicManager: Changing music in scene {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");

        // Stop previous music if enabled
        if (stopPreviousMusic)
        {
            Debug.Log("SceneMusicManager: Stopping previous music...");
            AudioManager.Instance.StopMusic(fadeTransition);
            
            // Wait for fade out if enabled
            if (fadeTransition)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        // Play scene music
        if (sceneMusic != null)
        {
            Debug.Log($"SceneMusicManager: Playing scene music: {sceneMusic.name}");
            AudioManager.Instance.PlayMusic(sceneMusic, fadeTransition);
        }
        else
        {
            Debug.LogWarning("SceneMusicManager: Scene music is not assigned!");
        }
    }
}
