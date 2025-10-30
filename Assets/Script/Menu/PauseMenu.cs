<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> origin/checkpoint-feature
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
<<<<<<< HEAD
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
        
=======

    void Update()
    {
>>>>>>> origin/checkpoint-feature
        // Khi nhấn Esc thì pause/unpause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
<<<<<<< HEAD
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
=======
                Resume();
            else
                Pause();
>>>>>>> origin/checkpoint-feature
        }
    }

    public void Resume()
    {
<<<<<<< HEAD
        // Play menu close sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();
            
=======
>>>>>>> origin/checkpoint-feature
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resume game
        GameIsPaused = false;
    }

    void Pause()
    {
<<<<<<< HEAD
        // Play menu open sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuOpen();
            
=======
>>>>>>> origin/checkpoint-feature
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // pause game
        GameIsPaused = true;
    }

<<<<<<< HEAD
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
=======
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // reset time scale trước khi load scene
>>>>>>> origin/checkpoint-feature
        SceneManager.LoadScene(0); // scene 0 = Main Menu
    }

    public void QuitGame()
    {
<<<<<<< HEAD
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
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();
            
        if (optionsMenuUI != null)
            optionsMenuUI.SetActive(false);
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }
=======
        Application.Quit();
    }
>>>>>>> origin/checkpoint-feature
}
