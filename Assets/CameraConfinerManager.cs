using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;

public class CameraConfinerManager : MonoBehaviour
{
    public static CameraConfinerManager Instance { get; private set; }

    private CinemachineVirtualCamera vcam;
    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        // Always set as instance for current scene - don't make persistent
        Instance = this;

        // Find the camera components - they might be on persistent objects
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam == null)
        {
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
        }
        
        confiner = FindObjectOfType<CinemachineConfiner2D>();
        
        Debug.Log("CameraConfinerManager: New instance created for scene");
    }

    private void Start()
    {
        // Wait a bit to ensure persistent objects are loaded
        StartCoroutine(InitializeCameraAfterDelay());
    }

    private IEnumerator InitializeCameraAfterDelay()
    {
        Debug.Log("CameraConfinerManager: Starting camera initialization...");
        
        // Wait a bit for scene to fully load
        yield return new WaitForSeconds(0.5f);
        
        // Try to find persistent camera and player multiple times
        float maxWaitTime = 3f;
        float waitInterval = 0.2f;
        float elapsedTime = 0f;
        
        while (elapsedTime < maxWaitTime)
        {
            // Check if there's a persistent camera from Character.cs
            CinemachineVirtualCamera[] allCameras = FindObjectsOfType<CinemachineVirtualCamera>();
            GameObject player = GameObject.FindWithTag("Player");
            
            Debug.Log($"CameraConfinerManager: Found {allCameras.Length} cameras, player: {(player != null ? player.name : "null")}");
            
            // Look for a camera that already has a player target
            foreach (var cam in allCameras)
            {
                if (cam.Follow != null && cam.Follow.CompareTag("Player"))
                {
                    vcam = cam;
                    Debug.Log("CameraConfinerManager: Found persistent camera with player target: " + cam.name);
                    ResetCameraSettings();
                    UpdateCameraTarget();
                    yield break; // Success, exit
                }
            }
            
            // If we found cameras and player, connect them
            if (allCameras.Length > 0 && player != null)
            {
                vcam = allCameras[0];
                Debug.Log("CameraConfinerManager: Connecting camera to player: " + vcam.name + " -> " + player.name);
                ResetCameraSettings();
                UpdateCameraTarget();
                yield break; // Success, exit
            }
            
            yield return new WaitForSeconds(waitInterval);
            elapsedTime += waitInterval;
        }
        
        // Final fallback
        Debug.LogWarning("CameraConfinerManager: Timeout reached, using fallback initialization");
        CinemachineVirtualCamera[] finalCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        if (finalCameras.Length > 0)
        {
            vcam = finalCameras[0];
            Debug.Log("CameraConfinerManager: Using fallback camera: " + vcam.name);
        }
        
        ResetCameraSettings();
        UpdateCameraTarget();
    }

    private void OnEnable()
    {
        // Subscribe to scene loaded event to reset camera when scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-initialize camera setup when scene loads
        StartCoroutine(InitializeCameraAfterDelay());
    }

    private IEnumerator DelayedCameraUpdate()
    {
        yield return new WaitForEndOfFrame();
        UpdateCameraTarget();
    }

    public void UpdateCameraTarget()
    {
        // Re-find camera if needed (in case it's on a persistent object)
        if (vcam == null)
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
            if (vcam == null)
            {
                vcam = FindObjectOfType<CinemachineVirtualCamera>();
            }
        }

        if (vcam != null)
        {
            // Try multiple ways to find the player, prioritizing persistent objects
            GameObject player = GameObject.FindWithTag("Player");
            
            // If not found by tag, try finding by name
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            
            // If still not found, try finding any object with Controller component
            if (player == null)
            {
                Controller controller = FindObjectOfType<Controller>();
                if (controller != null)
                {
                    player = controller.gameObject;
                }
            }

            if (player != null)
            {
                // Always update the target to ensure it's properly connected
                vcam.Follow = player.transform;
                vcam.LookAt = player.transform;
                vcam.PreviousStateIsValid = false; // Force immediate update
                Debug.Log("CameraConfinerManager: Camera updated to follow: " + player.name + " at position: " + player.transform.position);
            }
            else
            {
                Debug.LogWarning("Player not found when assigning Cinemachine target! Retrying in 0.1 seconds...");
                // Retry after a short delay in case player hasn't spawned yet
                StartCoroutine(RetryUpdateCameraTarget());
            }
        }
        else
        {
            Debug.LogWarning("CameraConfinerManager: Virtual Camera not found!");
        }
    }

    private System.Collections.IEnumerator RetryUpdateCameraTarget()
    {
        int maxRetries = 10;
        int retryCount = 0;
        
        while (retryCount < maxRetries)
        {
            yield return new WaitForSeconds(0.2f);
            retryCount++;
            
            if (vcam != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player == null)
                    player = GameObject.Find("Player");
                
                if (player == null)
                {
                    Controller controller = FindObjectOfType<Controller>();
                    if (controller != null)
                        player = controller.gameObject;
                }

                if (player != null)
                {
                    vcam.Follow = player.transform;
                    vcam.LookAt = player.transform;
                    vcam.PreviousStateIsValid = false;
                    Debug.Log("CameraConfinerManager: Camera connected to player (retry " + retryCount + "): " + player.name);
                    yield break; // Success, exit
                }
                else
                {
                    Debug.LogWarning("CameraConfinerManager: Player not found, retry " + retryCount + "/" + maxRetries);
                }
            }
        }
        
        Debug.LogError("CameraConfinerManager: Failed to find player after " + maxRetries + " retries!");
    }

    public void ResetCameraSettings()
    {
        // Re-find components in case they were destroyed during scene reload
        if (vcam == null)
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
            if (vcam == null)
            {
                vcam = FindObjectOfType<CinemachineVirtualCamera>();
            }
        }
        
        if (confiner == null)
            confiner = FindObjectOfType<CinemachineConfiner2D>();

        // Clear any existing boundary to prevent camera from being stuck
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = null;
            Debug.Log("Camera confiner boundary cleared for scene reset");
        }

        // Reset camera state but don't clear target if it's already set by Character.cs
        if (vcam != null)
        {
            // Force camera to update its position immediately
            vcam.PreviousStateIsValid = false;
            Debug.Log("Camera settings reset for new scene");
        }
    }

    public void SetMapBoundary(PolygonCollider2D newBoundary)
    {
        if (confiner == null)
        {
            confiner = FindObjectOfType<CinemachineConfiner2D>();
        }

        if (confiner != null && newBoundary != null)
        {
            confiner.m_BoundingShape2D = newBoundary;
            Debug.Log("Camera boundary set to: " + newBoundary.name);
        }
        else
        {
            Debug.LogWarning("Could not set camera boundary - confiner or boundary is null");
        }
    }

    public void ClearBoundary()
    {
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = null;
            Debug.Log("Camera boundary cleared");
        }
    }

    // Method to force camera to reconnect to player - useful for debugging
    public void ForceReconnectToPlayer()
    {
        Debug.Log("CameraConfinerManager: Force reconnect requested");
        StartCoroutine(InitializeCameraAfterDelay());
    }

    // Method to check current camera status
    public void LogCameraStatus()
    {
        Debug.Log("=== Camera Status ===");
        Debug.Log("vcam: " + (vcam != null ? vcam.name : "null"));
        Debug.Log("vcam.Follow: " + (vcam != null && vcam.Follow != null ? vcam.Follow.name : "null"));
        
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Player found: " + (player != null ? player.name : "null"));
        
        CinemachineVirtualCamera[] allCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        Debug.Log("Total cameras found: " + allCameras.Length);
        
        for (int i = 0; i < allCameras.Length; i++)
        {
            Debug.Log("Camera " + i + ": " + allCameras[i].name + " - Follow: " + (allCameras[i].Follow != null ? allCameras[i].Follow.name : "null"));
        }
        Debug.Log("==================");
    }
}
