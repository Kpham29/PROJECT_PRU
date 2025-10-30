using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject settingsPanel;
    
    private void Start()
    {
        // Show main menu, hide others
        ShowMainMenu();
    }
    
    public void playGame()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
=======
    public void playGame()
    {
>>>>>>> origin/checkpoint-feature
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
<<<<<<< HEAD
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        Application.Quit();
    }
    
    public void ShowOptions()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    public void ShowSettings()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    
    public void ShowMainMenu()
    {
        // Play button click sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuClose();
            
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    public void OnButtonHover()
    {
        // Optional: Play a subtle hover sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(null, 0.3f);
    }
=======
        Application.Quit();
    }
>>>>>>> origin/checkpoint-feature
}