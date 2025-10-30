using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject aboutPanel;
    
    [Header("Tab Buttons (Optional)")]
    [SerializeField] private Button controlsTabButton;
    [SerializeField] private Button audioTabButton;
    [SerializeField] private Button aboutTabButton;
    
    private void Start()
    {
        // Show controls by default
        ShowControls();
        
        // Setup tab button listeners
        if (controlsTabButton != null)
            controlsTabButton.onClick.AddListener(ShowControls);
        if (audioTabButton != null)
            audioTabButton.onClick.AddListener(ShowAudio);
        if (aboutTabButton != null)
            aboutTabButton.onClick.AddListener(ShowAbout);
    }
    
    public void ShowControls()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        SetActivePanel(controlsPanel);
    }
    
    public void ShowAudio()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        SetActivePanel(audioPanel);
    }
    
    public void ShowAbout()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
            
        SetActivePanel(aboutPanel);
    }
    
    private void SetActivePanel(GameObject activePanel)
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(controlsPanel == activePanel);
        if (audioPanel != null)
            audioPanel.SetActive(audioPanel == activePanel);
        if (aboutPanel != null)
            aboutPanel.SetActive(aboutPanel == activePanel);
    }
}
