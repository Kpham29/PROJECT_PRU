using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform currentPlayer; // Không cần kéo thủ công nữa
    public Vector3 offset = new Vector3(0, 0, -10f); // Offset so với player
    public float zoomSize = 10f; // Kích thước Orthographic Size (điều chỉnh zoom minimap)

    void Start()
    {
        FindCurrentPlayer();
        UpdateCameraSize();
    }

    void LateUpdate()
    {
        // Luôn kiểm tra player mới nhất (khi spawn/chuyển nhân vật)
        if (currentPlayer == null || currentPlayer.gameObject.activeInHierarchy == false)
        {
            FindCurrentPlayer();
        }

        if (currentPlayer != null)
        {
            // Di chuyển camera theo player (chỉ X,Y - giữ Z cố định)
            Vector3 targetPosition = currentPlayer.position + offset;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
    }

    void FindCurrentPlayer()
    {
        // Tìm tất cả GameObject có tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObj in players)
        {
            // Chọn player ĐANG ACTIVE (được spawn và đang chơi)
            if (playerObj.activeInHierarchy)
            {
                currentPlayer = playerObj.transform;
                Debug.Log("MinimapCamera: Found active player - " + playerObj.name);
                return;
            }
        }

        currentPlayer = null;
        Debug.LogWarning("MinimapCamera: No active player found!");
    }

    void UpdateCameraSize()
    {
        // Tự động set Orthographic Size cho minimap
        Camera cam = GetComponent<Camera>();
        if (cam != null && cam.orthographic)
        {
            cam.orthographicSize = zoomSize;
        }
    }

    // Public method để gọi từ code khác (khi spawn player)
    public void RefreshPlayer()
    {
        FindCurrentPlayer();
    }
}