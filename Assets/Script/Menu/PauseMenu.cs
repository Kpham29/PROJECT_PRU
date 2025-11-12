using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private bool isInitialized = false;
    private float lastEscapeTime = 0f;
    private const float escapeDelay = 0.3f; // Delay 0.3 giây giữa các lần nhấn ESC
    private bool isProcessingEscape = false; // Flag để ngăn double trigger

    void Start()
    {
        Debug.Log($"PauseMenu.Start() called on GameObject: {gameObject.name}");
        
        // Check how many PauseMenu instances exist
        PauseMenu[] instances = FindObjectsOfType<PauseMenu>();
        Debug.Log($"Number of PauseMenu instances in scene: {instances.Length}");
        if (instances.Length > 1)
        {
            Debug.LogWarning("Multiple PauseMenu instances detected! This may cause issues.");
        }
        
        // Ensure menus are hidden at start
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        if (optionsMenuUI != null)
            optionsMenuUI.SetActive(false);

        GameIsPaused = false;
        Time.timeScale = 1f;
        isInitialized = true;
    }
    void Update()
    {
        if (!isInitialized) return;
        
        // Khi nhấn Esc thì pause/unpause game
        if (Input.GetKeyDown(KeyCode.Escape) && !isProcessingEscape)
        {
            // Kiểm tra delay để tránh double input
            if (Time.unscaledTime - lastEscapeTime < escapeDelay)
            {
                Debug.Log("ESC pressed too quickly, ignoring...");
                return;
            }
            
            StartCoroutine(HandleEscapeInput());
        }
    }
    
    private IEnumerator HandleEscapeInput()
    {
        isProcessingEscape = true;
        lastEscapeTime = Time.unscaledTime;
        Debug.Log($"ESC pressed. GameIsPaused: {GameIsPaused}");
        
        // Wait for end of frame to ensure all input is processed
        yield return new WaitForEndOfFrame();
        
        if (GameIsPaused)
        {
            // If options menu is open, close it first
            if (optionsMenuUI != null && optionsMenuUI.activeSelf)
            {
                CloseOptions();
            }
            else
            {
                Resume();
            }
        }
        else
        {
            Pause();
        }
        
        // Wait a bit before allowing next input
        yield return new WaitForSecondsRealtime(0.2f);
        isProcessingEscape = false;
    }

    public void Resume()
    {
        Debug.Log("Resume() called");
        
        // Play menu close sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();
            
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
            
        Time.timeScale = 1f; // resume game
        
        // Resume music
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UnpauseMusic();
        }
        
        GameIsPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pause() called");
        Debug.Log($"pauseMenuUI: {pauseMenuUI?.name}, isNull: {pauseMenuUI == null}");
        
        // Play menu open sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuOpen();

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Debug.Log("Pause Menu UI activated");
        }
        else
        {
            Debug.LogError("pauseMenuUI is NULL! Please assign it in Inspector.");
        }
        
        Time.timeScale = 0f; // pause game
        
        // Pause music but keep UI sounds working
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PauseMusic();
        }
        
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f; // reset time scale
        
        // Unpause music before reloading
        if (AudioManager.Instance != null)
            AudioManager.Instance.UnpauseMusic();

        // Destroy existing CameraConfinerManager to force fresh initialization
        if (CameraConfinerManager.Instance != null)
        {
            Debug.Log("PauseMenu: Destroying CameraConfinerManager for restart");
            Destroy(CameraConfinerManager.Instance.gameObject);
        }

        // Reset persistent character position to initial spawn point
        GameObject persistentPlayer = GameObject.FindWithTag("Player");
        if (persistentPlayer != null)
        {
            Controller controller = persistentPlayer.GetComponent<Controller>();
            if (controller != null)
            {
                // Reset to initial spawn point
                controller.SetInitialSpawnPoint(controller.transform.position, controller.transform.rotation);
                Debug.Log("PauseMenu: Reset persistent player position for restart");
            }
        }
            
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void LoadMainMenu()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f; // reset time scale trước khi load scene
        
        // Unpause music before loading main menu
        if (AudioManager.Instance != null)
            AudioManager.Instance.UnpauseMusic();

        // Clean up persistent objects when returning to main menu
        GameObject persistentPlayer = GameObject.FindWithTag("Player");
        if (persistentPlayer != null)
        {
            Debug.Log("PauseMenu: Destroying persistent player when returning to main menu");
            Destroy(persistentPlayer);
        }

        // Destroy CameraConfinerManager instance
        if (CameraConfinerManager.Instance != null)
        {
            Debug.Log("PauseMenu: Destroying CameraConfinerManager when returning to main menu");
            Destroy(CameraConfinerManager.Instance.gameObject);
        }
            
        GameIsPaused = false;
       SceneManager.LoadScene(0); // scene 0 = Main Menu
    }

    public void QuitGame()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        Application.Quit();
    }

    public void OpenOptions()
    {
        Debug.Log("OpenOptions() called");
        Debug.Log($"pauseMenuUI: {pauseMenuUI?.name}, optionsMenuUI: {optionsMenuUI?.name}");
        
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Debug.Log("Pause Menu UI hidden");
        }
        else
        {
            Debug.LogWarning("pauseMenuUI is null!");
        }
        
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(true);
            Debug.Log("Options Menu UI shown");
        }
        else
        {
            Debug.LogWarning("optionsMenuUI is null!");
        }
    }

    public void CloseOptions()
    {
        // Play button close sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();

        if (optionsMenuUI != null)
            optionsMenuUI.SetActive(false);
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }
}
