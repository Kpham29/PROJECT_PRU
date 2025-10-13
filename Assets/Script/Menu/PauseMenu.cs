using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        // Khi nhấn Esc thì pause/unpause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resume game
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // pause game
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // reset time scale trước khi load scene
        SceneManager.LoadScene(0); // scene 0 = Main Menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
