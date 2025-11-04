using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private bool isInitialized = false;

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        }
    }

    public void Resume()
    {
        // Play menu close sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resume game
        GameIsPaused = false;
    }

    public void Pause()
    {
        // Play menu open sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuOpen();

        pauseMenuUI.SetActive(true); // <-- CHỈ BẬT PANEL NÀY LÊN
        Time.timeScale = 0f; // pause game
        AudioListener.pause = true; // <-- THÊM VÀO: Tạm dừng tất cả âm thanh
        GameIsPaused = true;

        // <-- XÓA CÁC DÒNG GÂY LỖI BÊN DƯỚI NÀY
        // if (pauseMenuUI != null)
        //     pauseMenuUI.SetActive(false);
        // if (optionsMenuUI != null)
        //     optionsMenuUI.SetActive(true);
    }

    public void RestartLevel()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f; // reset time scale
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        Time.timeScale = 1f; // reset time scale trước khi load scene
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
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        if (optionsMenuUI != null)
            optionsMenuUI.SetActive(true);
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
        Application.Quit();
    }
}
